using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TailendersApi.Repository.Entities;

namespace TailendersApi.Repository
{
    public interface IProfilesRepository
    {
        Task<ProfileEntity> GetProfile(string profileId);
        Task<ProfileEntity> UpsertProfile(ProfileEntity entity);
        Task DeleteProfile(string profileId);
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
                _db.Add<ProfileEntity>(entity);
            }
            else
            {
                _db.Update<ProfileEntity>(entity);
            }
            await _db.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteProfile(string profileId)
        {
            var profile = await GetProfile(profileId);

            if (profile == null)
                return;

            _db.Remove<ProfileEntity>(profile);
            await _db.SaveChangesAsync();
        }
    }
}
