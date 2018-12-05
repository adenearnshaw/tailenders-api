using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TailendersApi.Contracts;
using TailendersApi.Repository;
using TailendersApi.Repository.Entities;

namespace TailendersApi.WebApi.Managers
{
    public interface IPairingsManager
    {
        Task<MatchResult> SetPairingDescision(string profileId, string pairedProfileId, PairingDecision decision);
    }

    public class PairingsManager : IPairingsManager
    {
        private readonly IPairingsRepository _pairingsRepository;
        private readonly IMatchesRepository _matchesRepository;

        public PairingsManager(IPairingsRepository pairingsRepository,
                               IMatchesRepository matchesRepository)
        {
            _pairingsRepository = pairingsRepository;
            _matchesRepository = matchesRepository;
        }

        public async Task<MatchResult> SetPairingDescision(string profileId, string pairedProfileId, PairingDecision decision)
        {
            var existingPairing = await _pairingsRepository.GetPairing(profileId, pairedProfileId);

            if (existingPairing != null)
            {
                existingPairing.LastUpdated = DateTime.UtcNow;
                existingPairing.IsLiked = decision == PairingDecision.Liked;
                await _pairingsRepository.UpsertPairingData(existingPairing);
            }
            else
            {
                var newPairing = new PairingEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    ProfileId = profileId,
                    PairedProfileId = pairedProfileId,
                    IsLiked = decision == PairingDecision.Liked,
                    LastUpdated = DateTime.UtcNow
                };
                await _pairingsRepository.UpsertPairingData(newPairing);
            }

            var isMatch = await _pairingsRepository.CheckIfMatch(profileId, pairedProfileId);
            if (isMatch)
            {
                var matchGuid = Guid.NewGuid().ToString();
                var match = new MatchEntity
                {
                    Id = matchGuid,
                    MatchedAt = DateTime.UtcNow,
                    ProfileMatches = new List<ProfileMatchEntity>
                    {
                        new ProfileMatchEntity { ProfileId = profileId, MatchId = matchGuid },
                        new ProfileMatchEntity { ProfileId = pairedProfileId, MatchId = matchGuid }
                    },
                    MatchContactPreferences = new List<MatchContactPreferenceEntity>
                    {
                        new MatchContactPreferenceEntity { Id = Guid.NewGuid().ToString(), MatchId = matchGuid, ProfileId = profileId },
                        new MatchContactPreferenceEntity { Id = Guid.NewGuid().ToString(), MatchId = matchGuid, ProfileId = pairedProfileId }
                    }
                };
                await _matchesRepository.UpsertMatch(match);
            }

            var matchResult = new MatchResult
            {
                ProfileId = profileId,
                PairedProfileId = pairedProfileId,
                IsMatch = isMatch
            };
            return matchResult;
        }
    }
}