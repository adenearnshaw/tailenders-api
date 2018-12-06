using System;
using System.Linq;
using TailendersApi.Contracts;
using TailendersApi.Repository.Entities;

namespace TailendersApi.WebApi.Mappers
{
    public static class MatchDetailMapper
    {

        public static Func<string, MatchEntity, MatchDetail> ToContract = (profileId, entity) =>
        {
            var contactPrefs = entity.MatchContactPreferences.FirstOrDefault(mc => mc.ProfileId == profileId);
            var matchContactPrefs = entity.MatchContactPreferences.FirstOrDefault(mc => mc.ProfileId != profileId);

            if (contactPrefs == null || matchContactPrefs == null)
                return null;

            var contract = new MatchDetail
            {
                Id = entity.Id,
                ProfileId = profileId,
                MatchedAt = entity.MatchedAt,
                MatchedProfile = ProfileMapper.ToMatchedProfileContract(matchContactPrefs.Profile, matchContactPrefs.ContactDetailsVisible),
                UserContactDetailsVisible = contactPrefs.ContactDetailsVisible
            };
            return contract;
        };
    }
}
