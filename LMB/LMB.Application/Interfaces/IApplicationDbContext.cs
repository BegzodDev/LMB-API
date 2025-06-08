using LMB.Domain;
using Microsoft.EntityFrameworkCore;

namespace LMB.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Link> Links { get; set; }
        DbSet<LinkProfile> LinkProfiles { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
