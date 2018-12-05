namespace TailendersApi.Repository.Entities
{
    public class MatchContactPreferenceEntity
    {
        public string Id { get; set; }
        public string MatchId { get; set; }
        public string ProfileId { get; set; }
        public bool ContactDetailsVisible { get; set; }

        public MatchEntity Match { get; set; }
        public ProfileEntity Profile { get; set; }
    }
}