using LMB.Domain;

namespace LMB.Application.Interfaces
{
    public interface ILinkRepository
    {
        Task<List<Link>> GetAllByProfileIdAsync(Guid profileId);
        Task<Link?> GetByIdAsync(Guid id);
        Task AddAsync(Link link);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Link link);
    }
}
