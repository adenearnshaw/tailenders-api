using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace TailendersApi.Client
{
    public class ProfileImageUploader
    {
        private const string ImageUpdloadUrlFormat = "{0}/api/profiles/{1}/image";

        private readonly IClientSettings _settings;
        private readonly ICredentialsProvider _credentials;

        public ProfileImageUploader(IClientSettings settings,
                                    ICredentialsProvider credentialsProvider)
        {
            _settings = settings;
            _credentials = credentialsProvider;
        }

        /// <summary>
        /// Upload image
        /// </summary>
        /// <returns>Success state of upload</returns>
        /// <param name="image">Image stream (format must be jpeg)</param>
        public async Task<bool> UploadImage(Stream image)
        {
            var url = string.Format(ImageUpdloadUrlFormat, _settings.BaseUrl, _credentials.UserId);

            var fileStreamContent = new StreamContent(image);
            fileStreamContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
            {
                Name = "file",
                FileName = "profile_image.jpg"
            };
            fileStreamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(fileStreamContent);
                var response = await client.PostAsync(url, formData);
                return response.IsSuccessStatusCode;
            }
        }
    }
}
