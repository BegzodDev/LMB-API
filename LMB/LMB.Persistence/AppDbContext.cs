using LMB.Application.Interfaces;
using LMB.Domain;
using Microsoft.EntityFrameworkCore;


namespace LMB.Persistence
{
    public class AppDbContext : DbContext, IApplicationDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<User> Users { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<LinkProfile> LinkProfiles { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(x => x.Profile)
                .WithOne(z => z.User)
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

                    .HasIndex(x => x.UserId).IsUnique();
            modelBuilder.Entity<LinkProfile>()
                    .HasIndex(x => x.Username).IsUnique();
        }

    }
}
