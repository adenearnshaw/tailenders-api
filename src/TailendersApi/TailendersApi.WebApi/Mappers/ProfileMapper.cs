using System;
using System.Linq;
using TailendersApi.Contracts;
using TailendersApi.Repository.Entities;

namespace TailendersApi.WebApi.Mappers
{
    public static class ProfileMapper
    {
        public static Func<ProfileEntity, Profile> ToProfileContract = (entity) =>
        {
            var contract = new Profile
            {
                Id = entity.ID,
                Name = entity.Name,
                Age = entity.Age,
                ShowAge = entity.ShowAge,
                Location = entity.Location,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude,
                Bio = entity.Bio,
                FavouritePosition = entity.FavouritePosition,
                SearchShowInCategory = entity.SearchShowInCategory,
                SearchForCategory = entity.SearchForCategory,
                SearchRadius = entity.SearchRadius,
                SearchMinAge = entity.SearchMinAge,
                SearchMaxAge = entity.SearchMaxAge,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };

            if (entity.ProfileImages != null)
            {
                contract.Images = entity.ProfileImages
                                        .Select(ProfileImageMapper.ToContract)
                                        .ToList();
            }
            
            return contract;
        };

        public static Func<Profile, ProfileEntity> ToProfileEntity = (contract) =>
        {
            var entity = new ProfileEntity
            {
                ID = contract.Id,
                Name = contract.Name,
                Age = contract.Age,
                ShowAge = contract.ShowAge,
                Location = contract.Location,
                Latitude = contract.Latitude,
                Longitude = contract.Longitude,
                Bio = contract.Bio,
                FavouritePosition = contract.FavouritePosition,
                SearchShowInCategory = contract.SearchShowInCategory,
                SearchForCategory = contract.SearchForCategory,
                SearchRadius = contract.SearchRadius,
                SearchMinAge = contract.SearchMinAge,
                SearchMaxAge = contract.SearchMaxAge,
                CreatedAt = contract.CreatedAt,
                UpdatedAt = contract.UpdatedAt
            };
            return entity;
        };

        public static Func<ProfileEntity, SearchProfile> ToSearchProfileContract = (entity) =>
        {
            var contract = new SearchProfile
            {
                Id = entity.ID,
                Name = entity.Name,
                Age = entity.ShowAge ? entity.Age : 0,
                Location = entity.Location,
                Bio = entity.Bio,
                FavouritePosition = entity.FavouritePosition,
            };

            if (entity.ProfileImages != null)
            {
                contract.Images = entity.ProfileImages
                                        .Select(ProfileImageMapper.ToContract)
                                        .ToList();
            }
            
            return contract;
        };
    }
}
