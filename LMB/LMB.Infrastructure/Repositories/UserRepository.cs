using Microsoft.EntityFrameworkCore;
using LMB.Application.Interfaces;
using LMB.Persistence;
using LMB.Domain;

namespace LMB.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db) => _db = db;

        public async Task<User?> GetByEmailAsync(string email) =>
            await _db.Users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task<User?> GetByIdAsync(Guid id) =>
            await _db.Users.FindAsync(id);

        public async Task AddAsync(User user)
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> EmailExistsAsync(string email) =>
            await _db.Users.AnyAsync(u => u.Email == email);
    }
}
