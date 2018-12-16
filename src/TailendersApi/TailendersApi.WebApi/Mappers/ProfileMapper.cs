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
                Id = entity.Id,
                Name = entity.Name,
                Age = entity.Age,
                ShowAge = entity.ShowAge,
                Location = entity.Location,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude,
                Bio = entity.Bio,
                ContactDetails = entity.ContactDetails,
                FavouritePosition = entity.FavouritePosition,
                Gender = entity.Gender,
                SearchForCategory = entity.SearchForCategory,
                SearchRadius = entity.SearchRadius,
                SearchMinAge = entity.SearchMinAge,
                SearchMaxAge = entity.SearchMaxAge,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };

            if (entity.IsBlocked)
                contract.IsBlocked = true;

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
                Id = contract.Id,
                Name = contract.Name,
                Age = contract.Age,
                ShowAge = contract.ShowAge,
                Location = contract.Location,
                Latitude = contract.Latitude,
                Longitude = contract.Longitude,
                Bio = contract.Bio,
                ContactDetails = contract.ContactDetails,
                FavouritePosition = contract.FavouritePosition,
                Gender = contract.Gender,
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
                Id = entity.Id,
                Name = entity.Name,
                Age = entity.ShowAge ? entity.Age : 0,
                ShowAge = entity.ShowAge,
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

        public static Func<ProfileEntity, bool, MatchedProfile> ToMatchedProfileContract = (entity, mapContactDetails) =>
        {
            var contract = new MatchedProfile
            {
                Id = entity.Id,
                Name = entity.Name,
                Age = entity.ShowAge ? entity.Age : 0,
                ShowAge = entity.ShowAge,
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

            contract.ContactDetails = mapContactDetails
                ? entity.ContactDetails
                : String.Empty;

            return contract;
        };
    }
}
