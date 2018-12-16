using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TailendersApi.Contracts;
using TailendersApi.Repository;
using TailendersApi.Repository.Entities;
using TailendersApi.WebApi.Mappers;

namespace TailendersApi.WebApi.Managers
{
    public interface IMatchesManager
    {
        Task<List<MatchDetail>> GetProfileMatches(string profileId);
        Task<MatchDetail> UpdateMatch(MatchDetail match);
        Task UnmatchProfile(string profileId, string matchId);
        Task BlockMatch(string profileId, string matchId);
    }

    public class MatchesManager : IMatchesManager
    {
        private readonly IMatchesRepository _matchesRepository;
        private readonly IPairingsRepository _pairingsRepository;

        public MatchesManager(IMatchesRepository matchesRepository,
                              IPairingsRepository pairingsRepository)
        {
            _matchesRepository = matchesRepository;
            _pairingsRepository = pairingsRepository;
        }

        public async Task<List<MatchDetail>> GetProfileMatches(string profileId)
        {
            var results = await _matchesRepository.GetMatchesForProfile(profileId);
            var matches = results.Select(ma => MatchDetailMapper.ToContract(profileId, ma))
                                 .ToList();
            return matches;
        }

        public async Task<MatchDetail> UpdateMatch(MatchDetail match)
        {
            var entity = await _matchesRepository.GetMatch(match.Id);

            entity.MatchContactPreferences.First(mc => mc.ProfileId == match.ProfileId).ContactDetailsVisible =
                match.UserContactDetailsVisible;

            var updatedEntity = await _matchesRepository.UpsertMatch(entity);

            var updatedMatch = MatchDetailMapper.ToContract(match.ProfileId, updatedEntity);
            return updatedMatch;
        }

        public async Task UnmatchProfile(string profileId, string matchId)
        {
            var match = await _matchesRepository.GetMatch(matchId);
            var matchProfileId = match.ProfileMatches.FirstOrDefault(pm => pm.ProfileId != profileId)?.ProfileId ?? string.Empty;

            var pairing = await _pairingsRepository.GetPairing(profileId, matchProfileId);

            if (pairing != null)
            {
                pairing.IsLiked = false;
                pairing.UpdatedAt = DateTime.UtcNow;
            }

            await _matchesRepository.DeleteMatch(matchId);
            await _pairingsRepository.UpsertPairingData(pairing);
        }

        public async Task BlockMatch(string profileId, string matchId)
        {
            var match = await _matchesRepository.GetMatch(matchId);
            var matchProfileId = match.ProfileMatches.FirstOrDefault(pm => pm.ProfileId != profileId)?.ProfileId ?? string.Empty;

            var pairing = await _pairingsRepository.GetPairing(profileId, matchProfileId);

            if (pairing != null)
            {
                pairing.UpdatedAt = DateTime.UtcNow;
                pairing.IsLiked = false;
                pairing.IsBlocked = true;
                await _pairingsRepository.UpsertPairingData(pairing);
            }
            else
            {
                var newPairing = new PairingEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    ProfileId = profileId,
                    PairedProfileId = matchProfileId,
                    IsLiked = false,
                    IsBlocked = true,
                    UpdatedAt = DateTime.UtcNow
                };
                pairing = newPairing;
            }

            await _matchesRepository.DeleteMatch(matchId);
            await _pairingsRepository.UpsertPairingData(pairing);
        }
    }
}