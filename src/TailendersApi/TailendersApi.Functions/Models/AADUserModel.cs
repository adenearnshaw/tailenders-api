using Newtonsoft.Json;

namespace TailendersApi.Functions.Models
{
    public class AADUser
    {
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("objectId")]
        public string ObjectId { get; set; }
    }
}
