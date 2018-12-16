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


            modelBuilder.Entity<ProfileMatchEntity>().HasKey(pm => new { pm.ProfileId, pm.MatchId });
            modelBuilder.Entity<ProfileMatchEntity>()
                .HasOne<ProfileEntity>(pm => pm.Profile)
                .WithMany(pr => pr.ProfileMatches)
                .HasForeignKey(pm => pm.ProfileId);
            modelBuilder.Entity<ProfileMatchEntity>()
                .HasOne<MatchEntity>(pm => pm.Match)
                .WithMany(ma => ma.ProfileMatches)
                .HasForeignKey(pm => pm.MatchId);

            modelBuilder.Entity<MatchEntity>()
                .HasMany<MatchContactPreferenceEntity>(ma => ma.MatchContactPreferences)
                .WithOne(mc => mc.Match)
                .HasForeignKey(mc => mc.MatchId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MatchContactPreferenceEntity>()
                .HasOne<ProfileEntity>(mc => mc.Profile)
                .WithMany(p => p.MatchContactPreferences)
                .HasForeignKey(mc => mc.ProfileId);

            modelBuilder.Entity<BlockedProfileEntitiy>()
                .HasOne<ProfileEntity>(bp => bp.Profile)
                .WithOne()
                .HasForeignKey<BlockedProfileEntitiy>(bp => bp.ProfileId);

            modelBuilder.Entity<ReportedProfileEntity>()
                .HasOne<ProfileEntity>(bp => bp.Profile)
                .WithOne()
                .HasForeignKey<ReportedProfileEntity>(bp => bp.ProfileId);
        }

        public DbSet<ProfileEntity> Profiles { get; set; }
        public DbSet<ProfileImageEntity> ProfileImages { get; set; }
        public DbSet<PairingEntity> Pairings { get; set; }
        public DbSet<MatchEntity> Matches { get; set; }
        public DbSet<ProfileMatchEntity> ProfileMatches { get; set; }
        public DbSet<MatchContactPreferenceEntity> MatchContactPreferences { get; set; }
        public DbSet<BlockedProfileEntitiy> BlockedProfiles { get; set; }
        public DbSet<ReportedProfileEntity> ReportedProfiles { get; set; }
    }
}
