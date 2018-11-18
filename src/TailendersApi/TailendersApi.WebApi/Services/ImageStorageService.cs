using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace TailendersApi.WebApi.Services
{
    public interface IImageStorageService
    {
        Task<string> StoreImage(string filename, byte[] image);
    }

    public class ImageStorageService : IImageStorageService
    {
        private readonly IConfiguration _config;

        public ImageStorageService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<string> StoreImage(string filename, byte[] image)
        {
            var fileNameOnly = Path.GetFileName(filename);

            var url = string.Concat(_config["AzureBlobStorage:StorageUrl"], fileNameOnly);
            var creds = new StorageCredentials(_config["AzureBlobStorage:Account"], _config["AzureBlobStorage:Key"]);
            var blob = new CloudBlockBlob(new Uri(url), creds);

            if (await ShouldUpload(blob, image.Length))
            {
                await blob.UploadFromByteArrayAsync(image, 0, image.Length);
            }

            return url;
        }

        private async Task<bool> ShouldUpload(CloudBlockBlob blob, int imageLength)
        {
            bool shouldUpload = true;
            if (await blob.ExistsAsync())
            {
                await blob.FetchAttributesAsync();
                if (blob.Properties.Length == imageLength)
                {
                    shouldUpload = false;
                }
            }
            return shouldUpload;
        }
    }
}
