using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using TailendersApi.Client.Exceptions;
using TailendersApi.Contracts;

namespace TailendersApi.Client
{
    public interface IProfileImageUploader
    {
        Task<ProfileImage> UploadImage(Stream image);
    }

    public class ProfileImageUploader : IProfileImageUploader
    {
        private const string ImageUpdloadUrlFormat = "/api/profiles/{0}/image";

        private readonly IClientSettings _clientSettings;
        private readonly ICredentialsProvider _credentials;

        public ProfileImageUploader(IClientSettings settings,
                                    ICredentialsProvider credentialsProvider)
        {
            _clientSettings = settings;
            _credentials = credentialsProvider;
        }

        /// <summary>
        /// Upload image
        /// </summary>
        /// <returns>Success state of upload</returns>
        /// <param name="image">Image stream (format must be jpeg)</param>
        public async Task<ProfileImage> UploadImage(Stream image)
        {
            var url = string.Format(ImageUpdloadUrlFormat, _credentials.UserId);

            var fileStreamContent = new StreamContent(image);
            fileStreamContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
            {
                Name = "file",
                FileName = "profile_image.jpg"
            };
            fileStreamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

            using (var client = CreateClient())
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(fileStreamContent);
                var response = await client.PostAsync(url, formData);
                if (!response.IsSuccessStatusCode)
                {
                    throw new ImageUploadException(response.ReasonPhrase);
                }

                var json = await response.Content.ReadAsStringAsync();
                var profileImage = JsonConvert.DeserializeObject<ProfileImage>(json);
                return profileImage;
            }
        }

        private HttpClient CreateClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(_clientSettings.BaseUrl)
            };
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _credentials.AuthenticationToken);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}
