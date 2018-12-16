using System;

namespace TailendersApi.Repository.Entities
{
    public class BlockedProfileEntitiy
    {
        public string Id { get; set; }
        public string ProfileId { get; set; }
        public DateTime BlockedAt { get; set; }
        public int ReasonCode { get; set; }
        public string Description { get; set; }

        public ProfileEntity Profile { get; set; }

    }
}