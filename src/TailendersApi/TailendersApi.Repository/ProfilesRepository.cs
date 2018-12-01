﻿using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TailendersApi.Contracts;
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
            var result = await _db.Profiles.Include(pr => pr.ProfileImages)
                                           .FirstOrDefaultAsync<ProfileEntity>(pr => pr.Id == profileId);
            return result;
        }

        public async Task<ProfileEntity> UpsertProfile(ProfileEntity entity)
        {
            var profile = await GetProfile(entity.Id);

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
            var profile = await GetProfile(profileId);

            var searchQuery = _db.Profiles.Where(p => p.Age >= minAge
                                                   && p.Age <= maxAge);

            var searchQueryWithPreference = CalculateSearchProfilePredicate(searchQuery, 
                                                                            (Gender) profile.Gender,
                                                                            (SearchCategory) profile.SearchForCategory);

            var results = await searchQueryWithPreference.ToListAsync();

            return results;
        }

        private IQueryable<ProfileEntity> CalculateSearchProfilePredicate(IQueryable<ProfileEntity> query, Gender gender, SearchCategory searchFor)
        {
            if (gender == Gender.Male && searchFor == SearchCategory.Women) //Hetro male
            {
                query = query.Where(p => p.Gender == 1 && p.SearchForCategory == 0);
            }
            else if (gender == Gender.Male && searchFor == SearchCategory.Men) //Homo male
            {
                query = query.Where(p => p.Gender == 0 && p.SearchForCategory == 0);
            }
            else if (gender == Gender.Male && searchFor == SearchCategory.Both) //Bi male
            {
                query = query.Where(p => (p.Gender == 1 && p.SearchForCategory == 0) ||
                                         (p.Gender == 0 && p.SearchForCategory == 0));
            }
            else if(gender == Gender.Female && searchFor == SearchCategory.Men) //Hetro female
            {
                query = query.Where(p => p.Gender == 0 && p.SearchForCategory == 1);
            }
            else if (gender == Gender.Female && searchFor == SearchCategory.Women) //Homo female
            {
                query = query.Where(p => p.Gender == 1 && p.SearchForCategory == 1);
            }
            else if (gender == Gender.Female && searchFor == SearchCategory.Both) //Bi female
            {
                query = query.Where(p => (p.Gender == 0 && p.SearchForCategory == 1) ||
                                         (p.Gender == 1 && p.SearchForCategory == 1));
            }

            return query;
        }
    }
}
