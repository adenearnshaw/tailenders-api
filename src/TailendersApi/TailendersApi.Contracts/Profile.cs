using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TailendersApi.Contracts
{
    public class Profile
    {
        public Profile()
        {
            Images = new List<ProfileImage>();
        }

        [JsonProperty("profile_id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("age")]
        public int Age { get; set; }
        [JsonProperty("show_age")]
        public bool ShowAge { get; set; }
        [JsonProperty("location")]
        public string Location { get; set; }
        [JsonProperty("lat")]
        public double Latitude { get; set; }
        [JsonProperty("lon")]
        public double Longitude { get; set; }
        [JsonProperty("bio")]
        public string Bio { get; set; }
        [JsonProperty("favourite_position")]
        public int FavouritePosition { get; set; }

        [JsonProperty("search_show_in_category")]
        public int SearchShowInCategory { get; set; }
        [JsonProperty("search_category")]
        public int SearchForCategory { get; set; }
        [JsonProperty("search_radius")]
        public int SearchRadius { get; set; }
        [JsonProperty("search_min_age")]
        public int SearchMinAge { get; set; }
        [JsonProperty("search_max_age")]
        public int SearchMaxAge { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("images")]
        public List<ProfileImage> Images { get; set; }
    }
}
