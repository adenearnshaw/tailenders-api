using Newtonsoft.Json;
using TailendersApi.Contracts;

namespace TailendersApi.WebApi.Model
{
    public class ProfileReportInput
    {
        [JsonProperty("profileId")]
        public string ProfileId { get; set; }

        [JsonProperty("reportReason")]
        public ReportProfileReason ReportProfileReason { get; set; }
    }
}