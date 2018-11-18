using System;
using TailendersApi.Contracts;
using TailendersApi.Repository.Entities;

namespace TailendersApi.WebApi.Mappers
{
    public static class ProfileImageMapper
    {
        public static Func<ProfileImageEntity, ProfileImage> ToContract = (entity) =>
        {
            var contract = new ProfileImage
            {
                Id = entity.ID,
                ImageUrl = entity.ImageUrl,
                UpdatedAt = entity.LastUpdated
            };
            return contract;
        };

        public static Func<string, ProfileImage, ProfileImageEntity> ToEntity = (profileId, contract) =>
        {
            var entity = new ProfileImageEntity
            {
                ID = contract.Id,
                ProfileId = profileId,
                ImageUrl = contract.ImageUrl,
                LastUpdated = contract.UpdatedAt
            };
            return entity;
        };
    }
}
