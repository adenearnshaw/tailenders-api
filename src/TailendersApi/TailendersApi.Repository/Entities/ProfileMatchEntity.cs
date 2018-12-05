namespace TailendersApi.Repository.Entities
{
    public class ProfileMatchEntity
    {
        public string ProfileId { get; set; }
        public ProfileEntity Profile { get; set; }

        public string MatchId { get; set; }
        public MatchEntity Match { get; set; }
    }
}