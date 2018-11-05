using Microsoft.WindowsAzure.Storage.Table;

namespace TailendersApi.Repository.Entities
{
    public class ProfileEntity : TableEntity
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public bool ShowAge { get; set; }
        public string Location { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Bio { get; set; }
        public int FavouritePosition { get; set; }

        public int SearchShowInCategory { get; set; }
        public int SearchForCategory { get; set; }
        public int SearchRadius { get; set; }
        public int SearchMinAge { get; set; }
        public int SearchMaxAge { get; set; }
    }
}
