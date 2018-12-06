using System.Collections.Generic;

namespace TailendersApi.Contracts
{
    public interface IBasicProfile
    {
        string Id { get; set; }
        string Name { get; set; }
        int Age { get; set; }
        bool ShowAge { get; set; }
        string Location { get; set; }
        string Bio { get; set; }
        int FavouritePosition { get; set; }
        List<ProfileImage> Images { get; set; }
    }
}