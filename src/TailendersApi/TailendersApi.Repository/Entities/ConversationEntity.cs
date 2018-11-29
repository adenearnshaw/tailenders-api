using System;
using System.ComponentModel.DataAnnotations;

namespace TailendersApi.Repository.Entities
{
    public class ConversationEntity
    {
        [Key]
        public string ConversationId { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Data { get; set; }


    }
}
