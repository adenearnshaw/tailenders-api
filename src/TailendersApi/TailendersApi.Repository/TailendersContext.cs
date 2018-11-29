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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProfileEntity>()
                .HasMany<ProfileImageEntity>(pr => pr.ProfileImages)
                .WithOne(pi => pi.Profile)
                .HasForeignKey(pi => pi.ProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProfileEntity>()
                .HasMany<PairingEntity>(pr => pr.Pairings)
                .WithOne(pr => pr.Profile)
                .HasForeignKey(pa => pa.ProfileId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<ProfileEntity> Profiles { get; set; }
        public DbSet<ProfileImageEntity> ProfileImages { get; set; }
        public DbSet<PairingEntity> Pairings { get; set; }
        public DbSet<ConversationEntity> Conversations { get; set; }
    }
}
