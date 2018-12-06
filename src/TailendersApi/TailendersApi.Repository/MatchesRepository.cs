using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TailendersApi.Repository.Entities;

namespace TailendersApi.Repository
{
    public interface IMatchesRepository
    {
        Task<List<MatchEntity>> GetMatchesForProfile(string profileId);
        Task<MatchEntity> GetMatch(string matchId);
        Task<MatchEntity> UpsertMatch(MatchEntity entity);
        Task DeleteMatch(string matchId);
    }

    public class MatchesRepository : IMatchesRepository
    {
        private readonly TailendersContext _db;

        public MatchesRepository(TailendersContext context)
        {
            _db = context;
        }

        public async Task<List<MatchEntity>> GetMatchesForProfile(string profileId)
        {
            var matches = await _db.Matches
                                   .Include(m => m.MatchContactPreferences)
                                   .ThenInclude(mc => mc.Profile)
                                   .ThenInclude(pr => pr.ProfileImages)
                                   .Where(m => m.ProfileMatches.Any(pm => pm.ProfileId == profileId))
                                   .ToListAsync();
            return matches;
        }

        public async Task<MatchEntity> GetMatch(string matchId)
        {
            var match = await _db.Matches.FindAsync(matchId);
            return match;
        }

        public async Task<MatchEntity> UpsertMatch(MatchEntity entity)
        {
            var existingMatch = await _db.Matches.FindAsync(entity.Id);

            if (existingMatch == null)
            {
                await _db.Matches.AddAsync(entity);
            }
            else
            {
                _db.MatchContactPreferences.UpdateRange(entity.MatchContactPreferences);
                _db.Matches.Update(entity);
            }

            await _db.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteMatch(string matchId)
        {
            var match = await GetMatch(matchId);
            _db.Matches.Remove(match);
            await _db.SaveChangesAsync();
        }
    }
}