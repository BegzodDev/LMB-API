using LMB.Domain;
using Microsoft.EntityFrameworkCore;


namespace LMB.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<User> Users => Set<User>();
        public DbSet<Link> Links => Set<Link>();
        public DbSet<LinkProfile> LinkProfiles => Set<LinkProfile>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User ↔ LinkProfile (1:1)
            modelBuilder.Entity<User>()
                .HasOne(x=>x.Profile)
                .WithOne(z=>z.User)
                .HasForeignKey<LinkProfile>(p => p.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LinkProfile>()
                .HasMany(x => x.Links)
                .WithOne(z => z.LinkProfile)
                .HasForeignKey(x => x.LinkProfileId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasIndex(x => x.Email).IsUnique();

            modelBuilder.Entity<LinkProfile>()

                .HasIndex(x=>x.UserId).IsUnique();
            modelBuilder.Entity<LinkProfile>()
                .HasIndex(x => x.Username).IsUnique();

                
        }

    }
}
