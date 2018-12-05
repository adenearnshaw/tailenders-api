using Newtonsoft.Json;
using TailendersApi.Contracts;

namespace TailendersApi.WebApi.Model
{
    public class PairingDecisionInput
    {
        [JsonProperty("pairProfileId")]
        public string PairProfileId { get; set; }

        [JsonProperty("decision")]
        public PairingDecision Decision { get; set; }
    }
}