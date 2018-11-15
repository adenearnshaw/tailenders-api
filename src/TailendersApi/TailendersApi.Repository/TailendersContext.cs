using Microsoft.EntityFrameworkCore;
using TailendersApi.Repository.Entities;

namespace TailendersApi.Repository
{
    public class TailendersContext : DbContext
    {
        public TailendersContext(DbContextOptions<TailendersContext> options)
            : base(options)
        {
        }

        public DbSet<ProfileEntity> Profiles { get; set; }
        public DbSet<PairingEntity> Pairings { get; set; }
        public DbSet<ConversationEntity> Conversations { get; set; }
    }
}
