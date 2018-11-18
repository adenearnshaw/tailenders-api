using System.Threading.Tasks;
using TailendersApi.Repository.Entities;

namespace TailendersApi.Repository
{
    public interface IProfileImagesRepository
    {
        Task<ProfileImageEntity> GetImage(string imageId);
        Task<ProfileImageEntity> UpsertImage(ProfileImageEntity entity);
        Task DeleteImage(string imageId);
    }

    public class ProfileImagesRepository : IProfileImagesRepository
    {
        private readonly TailendersContext _db;

        public ProfileImagesRepository(TailendersContext context)
        {
            _db = context;
        }

        public async Task<ProfileImageEntity> GetImage(string imageId)
        {
            var result = await _db.FindAsync<ProfileImageEntity>(imageId);
            return result;
        }

        public async Task<ProfileImageEntity> UpsertImage(ProfileImageEntity entity)
        {
            var image = await GetImage(entity.ID);

            if (image == null)
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

        public async Task DeleteImage(string imageId)
        {
            var image = await GetImage(imageId);
            if (image == null)
                return;

            _db.Remove(image);
            await _db.SaveChangesAsync();
        }
    }
}
