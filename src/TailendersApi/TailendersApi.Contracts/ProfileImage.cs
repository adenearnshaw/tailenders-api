using System;
using Newtonsoft.Json;

namespace TailendersApi.Contracts
{
    public class ProfileImage
    {
        [JsonProperty("image_id")]
        public string Id { get; set; }
        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
