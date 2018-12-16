using System;

namespace TailendersApi.Repository.Entities
{
    public class ReportedProfileEntity
    {
        public string Id { get; set; }
        public string ProfileId { get; set; }
        public DateTime ReportedAt { get; set; }
        public int ReasonCode { get; set; }
        public bool HasBeenReviewed { get; set; }
        public string ReviewNotes { get; set; }

        public ProfileEntity Profile { get; set; }
    }
}