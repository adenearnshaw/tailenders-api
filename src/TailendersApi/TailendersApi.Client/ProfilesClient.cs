using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TailendersApi.Client.Exceptions;
using TailendersApi.Contracts;

namespace TailendersApi.Client
{
    public interface IProfilesClient
    {
        Task<Profile> GetProfile();
        Task<Profile> CreateProfile(Profile createProfile);
        Task<Profile> UpdateProfile(Profile updatedProfile);
        Task DeleteProfile();
    }

    public class ProfilesClient : ClientBase, IProfilesClient
    {
        private const string Profiles_Get_Url = "/api/profiles/{0}";
        private const string Profiles_Create_Url = "/api/profiles/";
        private const string Profiles_Update_Url = "/api/profiles/";
        private const string Profiles_Delete_Url = "/api/profiles/{0}";
        
        public ProfilesClient(IClientSettings settings,
                              ICredentialsProvider credentials)
            : base(settings, credentials)
        {
        }

        public async Task<Profile> GetProfile()
        {
            var url = string.Format(Profiles_Get_Url, Credentials.UserId);

            var response = await Send(HttpMethod.Get, url);

            if (!response.IsSuccessStatusCode)
                return default(Profile);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.NoContent && string.IsNullOrWhiteSpace(responseContent))
                throw new ProfileDoesntExistException();
            
            var profile = JsonConvert.DeserializeObject<Profile>(responseContent);
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
            var url = string.Format(Profiles_Delete_Url, Credentials.UserId);
            await Delete(url);
        }
    }

}
