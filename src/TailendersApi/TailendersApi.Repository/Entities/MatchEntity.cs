using System;
using System.Collections.Generic;
using System.Text;

namespace TailendersApi.Repository.Entities
{
    public class MatchEntity
    {
        public string Id { get; set; }
        public DateTime MatchedAt { get; set; }

        public List<MatchContactPreferenceEntity> MatchContactPreferences { get; set; }
        public List<ProfileMatchEntity> ProfileMatches { get; set; }
    }
}
