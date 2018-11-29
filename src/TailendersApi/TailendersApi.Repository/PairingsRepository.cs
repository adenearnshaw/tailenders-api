using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TailendersApi.Repository.Entities;

namespace TailendersApi.Repository
{
    public interface IPairingsRepository
    {
        Task<IEnumerable<PairingEntity>> GetPairingsForUser(string profileId);
        Task<PairingEntity> UpsertPairingData(PairingEntity entity);
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
            var results = await _db.FindAsync<ICollection<PairingEntity>>(profileId);
            return results;
        }

        public async Task<PairingEntity> UpsertPairingData(PairingEntity entity)
        {
            var pairing = (await _db.FindAsync<ICollection<PairingEntity>>(entity.Id)).FirstOrDefault();

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
