using System.Collections.Generic;
using System.Threading.Tasks;
using TailendersApi.Contracts;

namespace TailendersApi.Client
{
    public interface IMatchesClient
    {
        Task<List<MatchDetail>> GetMatches();
        Task<MatchDetail> UpdateMatch(MatchDetail matchDetail);
        Task Unmatch(MatchDetail matchDetail);
    }

    public class MatchesClient : ClientBase, IMatchesClient
    {
        private const string Profiles_GetMatches_Url = "/api/profiles/{0}/matches";
        private const string Matches_Update_Url = "/api/matches/";
        private const string Matches_Delete_Url = "/api/matches/{0}/unmatch";

        public MatchesClient(IClientSettings clientSettings, ICredentialsProvider credentialsProvider) 
            : base(clientSettings, credentialsProvider)
        {
        }

        public async Task<List<MatchDetail>> GetMatches()
        {
            var url = string.Format(Profiles_GetMatches_Url, Credentials.UserId);
            var matches = await Get<List<MatchDetail>>(url);
            return matches;
        }

        public async Task<MatchDetail> UpdateMatch(MatchDetail matchDetail)
        {
            var url = Matches_Update_Url;
            var updatedMatchDetail = await Put<MatchDetail, MatchDetail>(url, matchDetail);
            return updatedMatchDetail;
        }

        public async Task Unmatch(MatchDetail matchDetail)
        {
            var url = string.Format(Matches_Delete_Url, matchDetail.Id);
            await Delete(url);
        }
    }
}