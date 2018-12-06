using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TailendersApi.Repository.Entities;

namespace TailendersApi.Repository
{
    public interface IPairingsRepository
    {
        Task<IEnumerable<PairingEntity>> GetPairingsForUser(string profileId);
        Task<PairingEntity> GetPairing(string profileId, string pairedProfileId);
        Task<PairingEntity> UpsertPairingData(PairingEntity entity);
        Task<bool> CheckIfMatch(string profileId, string pairedProfileId);
        Task DeletePairings(string profileId);
    }

    public class PairingsRepository : IPairingsRepository
    {
        private readonly TailendersContext _db;

        public PairingsRepository(TailendersContext context)
        {
            _db = context;
        }

        public async Task<IEnumerable<PairingEntity>> GetPairingsForUser(string profileId)
        {
            var results = await _db.Pairings.Where(pa => pa.ProfileId == profileId).ToListAsync();
            return results;
        }

        public async Task<PairingEntity> GetPairing(string profileId, string pairedProfileId)
        {
            var results =
                await _db.Pairings.Where(pa => pa.ProfileId == profileId && pa.PairedProfileId == pairedProfileId).ToListAsync();
            return results.FirstOrDefault();
        }

        public async Task<bool> CheckIfMatch(string profileId, string pairedProfileId)
        {
            var results = await _db.Pairings.Where(pa => (pa.ProfileId == profileId && pa.PairedProfileId == pairedProfileId) ||
                                                   (pa.ProfileId == pairedProfileId && pa.PairedProfileId == profileId))
                                            .ToListAsync();

            if (results.Count >= 2)
            {
                return results.All(pa => pa.IsLiked);
            }

            return false;
        }

        public async Task<PairingEntity> UpsertPairingData(PairingEntity entity)
        {
            var pairing = await _db.FindAsync<PairingEntity>(entity.Id);

            if (pairing == null)
            {
                _db.Add<PairingEntity>(entity);
            }
            else
            {
                _db.Update<PairingEntity>(entity);
            }
            await _db.SaveChangesAsync();

            return entity;
        }

        public async Task DeletePairings(string profileId)
        {
            var pairings = _db.Pairings.Where(p => p.ProfileId == profileId);
            _db.RemoveRange(pairings);
            await _db.SaveChangesAsync();
        }
    }
}
