using System;
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