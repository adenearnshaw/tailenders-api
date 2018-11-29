using System;

namespace TailendersApi.Repository.Entities
{
    public class ProfileImageEntity
    {
        public string Id { get; set; }
        public string ProfileId { get; set; }
        public string ImageUrl { get; set; }
        public DateTime LastUpdated { get; set; }

        public ProfileEntity Profile { get; set; }
    }
}
