using Microsoft.EntityFrameworkCore;
using LMB.Application.Interfaces;
using LMB.Persistence;
using LMB.Domain;

namespace LMB.Infrastructure.Repositories
{
    public class LinkRepository : ILinkRepository
    {
        private readonly AppDbContext _db;
        public LinkRepository(AppDbContext db) => _db = db;

        public async Task<List<Link>> GetAllByProfileIdAsync(Guid profileId) =>
            await _db.Links.Where(l => l.LinkProfileId == profileId).ToListAsync();

        public async Task<Link?> GetByIdAsync(Guid id) =>
            await _db.Links.FindAsync(id);

        public async Task AddAsync(Link link)
        {
            await _db.Links.AddAsync(link);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var link = await _db.Links.FindAsync(id);
            if (link is not null)
            {
                _db.Links.Remove(link);
                await _db.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Link link)
        {
            _db.Links.Update(link);
            await _db.SaveChangesAsync();
        }
    }
}
