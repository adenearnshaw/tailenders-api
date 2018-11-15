using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TailendersApi.Contracts;
using TailendersApi.Repository;
using TailendersApi.WebApi.Exceptions;
using TailendersApi.WebApi.Mappers;

namespace TailendersApi.WebApi.Managers
{
    public interface IProfilesManager
    {
        Task<Profile> GetProfile(string userId);
        Task<Profile> AddProfile(Profile model);
        Task<Profile> UpdateProfile(Profile model);
        Task DeleteProfile(string userId);
    }

    public class ProfilesManager : IProfilesManager
    {
        private readonly IProfilesRepository _profilesRepository;
        private readonly IPairingsRepository _pairingsRepository;

        public ProfilesManager(IProfilesRepository profilesRepository,
                                IPairingsRepository pairingsRepository)
        {
            _profilesRepository = profilesRepository;
            _pairingsRepository = pairingsRepository;
        }

        public async Task<Profile> GetProfile(string profileId)
        {
            var profileEntity = await _profilesRepository.GetProfile(profileId);
            if (profileEntity == null)
                return null;

            var contract = ProfileMapper.ToContract(profileEntity);
            return contract;
        }

        public async Task<Profile> AddProfile(Profile model)
        {
            var updatedModel = await UpdateProfile(model);
            return updatedModel;
        }

        public async Task<Profile> UpdateProfile(Profile model)
        {
            var entity = await _profilesRepository.UpsertProfile(ProfileMapper.ToEntity(model));
            var contract = ProfileMapper.ToContract(entity);
            return contract;
        }

        public async Task DeleteProfile(string profileId)
        {
            await _pairingsRepository.DeletePairings(profileId);
            await _profilesRepository.DeleteProfile(profileId);
        }
    }
}
