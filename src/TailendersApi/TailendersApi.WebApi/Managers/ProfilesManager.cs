using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TailendersApi.Contracts;
using TailendersApi.Repository;
using TailendersApi.Repository.Entities;
using TailendersApi.WebApi.Mappers;
using TailendersApi.WebApi.Services;

namespace TailendersApi.WebApi.Managers
{
    public interface IProfilesManager
    {
        Task<Profile> GetProfile(string profileId);
        Task<Profile> AddProfile(Profile model);
        Task<Profile> UpdateProfile(Profile model);
        Task DeleteProfile(string userId);

        Task<ProfileImage> UploadProfileImage(string profileId, byte[] image);

        Task<IEnumerable<SearchProfile>> SearchForProfiles(string profileId);
    }

    public class ProfilesManager : IProfilesManager
    {
        private readonly IProfilesRepository _profilesRepository;
        private readonly IProfileImagesRepository _profileImagesRepository;
        private readonly IPairingsRepository _pairingsRepository;
        private readonly IImageStorageService _imageStorageService;

        public ProfilesManager(IProfilesRepository profilesRepository,
                               IProfileImagesRepository profileImagesRepository,
                               IPairingsRepository pairingsRepository,
                               IImageStorageService imageStorageService)
        {
            _profilesRepository = profilesRepository;
            _profileImagesRepository = profileImagesRepository;
            _pairingsRepository = pairingsRepository;
            _imageStorageService = imageStorageService;
        }

        public async Task<Profile> GetProfile(string profileId)
        {
            var profileEntity = await _profilesRepository.GetProfile(profileId);
            if (profileEntity == null)
                return null;

            var contract = ProfileMapper.ToProfileContract(profileEntity);
            return contract;
        }

        public async Task<Profile> AddProfile(Profile model)
        {
            var updatedModel = await UpdateProfile(model);
            return updatedModel;
        }

        public async Task<Profile> UpdateProfile(Profile model)
        {
            var entity = await _profilesRepository.UpsertProfile(ProfileMapper.ToProfileEntity(model));
            var contract = ProfileMapper.ToProfileContract(entity);
            return contract;
        }

        public async Task DeleteProfile(string profileId)
        {
            await _pairingsRepository.DeletePairings(profileId);
            await _profilesRepository.DeleteProfile(profileId);
        }

        public async Task<ProfileImage> UploadProfileImage(string profileId, byte[] image)
        {
            var fileName = $"{Guid.NewGuid()}.jpg";
            var blobUrl = await _imageStorageService.StoreImage(fileName, image);

            var entity = new ProfileImageEntity
            {
                ImageUrl = blobUrl,
                LastUpdated = DateTime.UtcNow,
                ProfileId = profileId
            };

            var updatedEntity = await _profileImagesRepository.UpsertImage(entity);

            var contract = ProfileImageMapper.ToContract(updatedEntity);
            return contract;
        }

        public async Task<IEnumerable<SearchProfile>> SearchForProfiles(string profileId)
        {
            var profile = await GetProfile(profileId);

            var categories = profile.SearchForCategory == (int)SearchCategory.Both 
                                    ? new int[] { (int)SearchCategory.Men, (int)SearchCategory.Women}
                                    : new int[] { profile.SearchForCategory };

            var searchResults = await _profilesRepository.SearchForProfiles(profileId,
                                                                            profile.SearchMinAge,
                                                                            profile.SearchMaxAge,
                                                                            categories);

            var profiles = searchResults.Select(ProfileMapper.ToSearchProfileContract);
            return profiles;
        }
    }
}
