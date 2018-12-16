using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TailendersApi.Client.Exceptions;
using TailendersApi.Contracts;

namespace TailendersApi.Client
{
    public interface IPairingsClient
    {
        Task<IList<SearchProfile>> SearchForProfiles();
        Task<MatchResult> SendPairDecision(string pairedProfileId, PairingDecision decision);
        Task BlockPairing(string pairedProfileId);
    }

    public class PairingsClient : ClientBase, IPairingsClient
    {
        private const string Pairings_Get_Url = "/api/pairings/{0}";
        private const string Pairings_PostDecision_Url = "/api/pairings/{0}/decision";
        private const string Pairings_Block_Url = "/api/pairings/{0}/block";

        public PairingsClient(IClientSettings settings, 
                                 ICredentialsProvider credentials)
            : base(settings, credentials)
        {
        }

        public async Task<IList<SearchProfile>> SearchForProfiles()
        {
            var url = string.Format(Pairings_Get_Url, Credentials.UserId);
            var profiles = await Get<IList<SearchProfile>>(url);
            return profiles;
        }

        public async Task<MatchResult> SendPairDecision(string pairedProfileId, PairingDecision decision)
        {
            var url = string.Format(Pairings_PostDecision_Url, Credentials.UserId);
            var data = new 
            {
                pairProfileId = pairedProfileId, 
                decision = decision
            };

            var matchResult = await Post<object, MatchResult>(url, data);

            return matchResult;
        }

        public async Task BlockPairing(string pairedProfileId)
        {
            var url = string.Format(Pairings_Block_Url, Credentials.UserId);
            var response = await Send(HttpMethod.Post, url, pairedProfileId);

            if (!response.IsSuccessStatusCode)
                throw new ClientException((int)response.StatusCode, response.ReasonPhrase);
        }
    }
}
