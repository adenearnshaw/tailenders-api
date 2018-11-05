using Microsoft.WindowsAzure.Storage.Table;

namespace TailendersApi.Repository.Entities
{
    public class ProfileImageEntity: TableEntity
    {
        public string UserId { get; set; }
        public string PhotoUrl { get; set; }
    }
}
