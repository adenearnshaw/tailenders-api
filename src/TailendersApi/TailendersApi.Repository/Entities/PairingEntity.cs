using System;

namespace TailendersApi.Repository.Entities
{
    public class PairingEntity
    {
        public string Id { get; set; }
        public string ProfileId { get; set; }
        public string PairedProfileId { get; set; }
        public bool IsLiked { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime LastUpdated { get; set; }

        public ProfileEntity Profile { get; set; }
        
    }
}
