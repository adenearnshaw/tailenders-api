using System.Collections.Generic;
using System.Threading.Tasks;
using TailendersApi.Contracts;

namespace TailendersApi.Client
{
    public interface IPairingsRetriever
    {
        Task<IList<SearchProfile>> SearchForProfiles();
        Task<MatchResult> SendPairDecision(string pairedProfileId, PairingDecision decision);
    }

    public class PairingsRetriever : RetrieverBase, IPairingsRetriever
    {
        private const string Pairings_Get_Url = "/api/pairings/{0}";
        private const string Pairings_PostDecision_Url = "/api/pairings/{0}";

        private readonly ICredentialsProvider _credentials;

        public PairingsRetriever(IClientSettings settings, 
                                 ICredentialsProvider credentials)
            : base(settings, credentials)
        {
            _credentials = credentials;
        }

        public async Task<IList<SearchProfile>> SearchForProfiles()
        {
            var url = string.Format(Pairings_Get_Url, _credentials.UserId);
            var profiles = await Get<IList<SearchProfile>>(url);
            return profiles;
        }

        public async Task<MatchResult> SendPairDecision(string pairedProfileId, PairingDecision decision)
        {
            var url = string.Format(Pairings_PostDecision_Url, _credentials.UserId);
            var data = new 
            {
                pairProfileId = pairedProfileId, 
                decision = decision
            };

            var matchResult = await Post<object, MatchResult>(url, data);

            return matchResult;
        }
    }
}
