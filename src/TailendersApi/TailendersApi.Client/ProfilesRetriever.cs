using System.Threading.Tasks;
using TailendersApi.Contracts;

namespace TailendersApi.Client
{
    public interface IProfilesRetriever
    {
        Task<Profile> GetProfile();
        Task<Profile> CreateProfile(Profile createProfile);
        Task<Profile> UpdateProfile(Profile updatedProfile);
        Task DeleteProfile();
    }

    public class ProfilesRetriever : RetrieverBase, IProfilesRetriever
    {
        private const string Profiles_Get_Url = "/api/profiles/me";
        private const string Profiles_Create_Url = "/api/profiles/";
        private const string Profiles_Update_Url = "/api/profiles/";
        private const string Profiles_Delete_Url = "/api/profiles/{0}";

        private readonly ICredentialsProvider _credentials;

        public ProfilesRetriever(IClientSettings settings,
                                 ICredentialsProvider credentials)
            : base(settings, credentials)
        {
            _credentials = credentials;
        }

        public async Task<Profile> GetProfile()
        {
            var profile = await Get<Profile>(Profiles_Get_Url);
            return profile;
        }

        public async Task<Profile> CreateProfile(Profile createProfile)
        {
            var profile = await Post<Profile, Profile>(Profiles_Create_Url, createProfile);
            return profile;
        }

        public async Task<Profile> UpdateProfile(Profile updatedProfile)
        {
            var profile = await Put<Profile, Profile>(Profiles_Update_Url, updatedProfile);
            return profile;
        }

        public async Task DeleteProfile()
        {
            await Delete(Profiles_Delete_Url);
        }
    }

}
