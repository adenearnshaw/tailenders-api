using System;
using Newtonsoft.Json;

namespace TailendersApi.Contracts
{
    public class MatchDetail
    {
        [JsonProperty("match_id")]
        public string Id { get; set; }

        [JsonProperty("profile_id")]
        public string ProfileId { get; set; }

        [JsonProperty("matched_at")]
        public DateTime MatchedAt { get; set; }

        [JsonProperty("user_contact_details_visible")]
        public bool UserContactDetailsVisible { get; set; }
       
        [JsonProperty("matched_profile")]
        public MatchedProfile MatchedProfile { get; set; }
    }
}
