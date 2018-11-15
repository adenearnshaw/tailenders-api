using System;
using TailendersApi.Contracts;
using TailendersApi.Repository.Entities;

namespace TailendersApi.WebApi.Mappers
{
    public static class ProfileMapper
    {
        public static Func<ProfileEntity, Profile> ToContract = (entity) =>
        {
            var contract = new Profile
            {
                Id = entity.ID,
                Name = entity.Name
            };
            return contract;
        };

        public static Func<Profile, ProfileEntity> ToEntity = (contract) =>
        {
            var entity = new ProfileEntity
            {
                ID = contract.Id,
                Name = contract.Name
            };
            return entity;
        };
    }
}
