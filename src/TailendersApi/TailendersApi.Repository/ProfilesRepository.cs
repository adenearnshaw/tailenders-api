using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TailendersApi.Repository.Entities;

namespace TailendersApi.Repository
{
    public interface IProfilesRepository
    {
        Task<ProfileEntity> GetProfile(string profileId);
        Task<ProfileEntity> UpsertProfile(ProfileEntity entity);
        Task DeleteProfile(string profileId);
        Task<List<ProfileEntity>> SearchForProfiles(string profileId, int minAge, int maxAge, int[] categories);
    }

    public class ProfilesRepository : IProfilesRepository
    {
        private readonly TailendersContext _db;

        public ProfilesRepository(TailendersContext context)
        {
            _db = context;
        }


        public async Task<ProfileEntity> GetProfile(string profileId)
        {
            var result = await _db.FindAsync<ProfileEntity>(profileId);
            return result;
        }

        public async Task<ProfileEntity> UpsertProfile(ProfileEntity entity)
        {
            var profile = await GetProfile(entity.ID);

            if (profile == null)
            {
                _db.Add(entity);
            }
            else
            {
                _db.Update(entity);
            }
            await _db.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteProfile(string profileId)
        {
            var profile = await GetProfile(profileId);

            if (profile == null)
                return;

            _db.Remove(profile);
            await _db.SaveChangesAsync();
        }

        public async Task<List<ProfileEntity>> SearchForProfiles(string profileId, int minAge, int maxAge, int[] categories)
        {
            return await Task.Run(() =>
            {
                var searchProcName = "SearchForProfiles";
                var take = 20;

                var userIdParam = new SqlParameter("@userId", profileId);
                var takeParam = new SqlParameter("@take", take);
                var minAgeParam = new SqlParameter("@minAge", minAge);
                var maxAgeParam = new SqlParameter("@maxAge", maxAge);
                var categoriesParam = new SqlParameter("@categories", string.Join(',', categories));

                var results = _db.Profiles.FromSql($"{searchProcName} @p0, @p1, @p2, @p3, @p4",
                                                   userIdParam,
                                                   takeParam,
                                                   minAgeParam,
                                                   maxAgeParam,
                                                   categoriesParam);
                return results.ToList();
            });
        }
    }
}
