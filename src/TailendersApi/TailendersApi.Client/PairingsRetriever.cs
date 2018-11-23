using System.Collections.Generic;
using System.Threading.Tasks;
using TailendersApi.Contracts;

namespace TailendersApi.Client
{
    public interface IPairingsRetriever
    {
        Task<IList<SearchProfile>> SearchForProfiles();
    }

    public class PairingsRetriever : RetrieverBase, IPairingsRetriever
    {
        private const string Pairings_Get_Url = "/api/pairings/{0}";

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
    }
}
