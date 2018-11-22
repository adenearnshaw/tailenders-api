using System.Collections.Generic;
using Newtonsoft.Json;

namespace TailendersApi.Contracts
{
    public class SearchProfile
    {
        [JsonProperty("profile_id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("age")]
        public int Age { get; set; }
        [JsonProperty("location")]
        public string Location { get; set; }
        [JsonProperty("bio")]
        public string Bio { get; set; }
        [JsonProperty("favourite_position")]
        public int FavouritePosition { get; set; }

        [JsonProperty("images")]
        public List<ProfileImage> Images { get; set; }

    }
}
