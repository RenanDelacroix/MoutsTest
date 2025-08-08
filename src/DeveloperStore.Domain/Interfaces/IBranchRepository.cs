using DeveloperStore.Domain.Entities;

namespace DeveloperStore.Domain.Interfaces
{
    public interface IBranchRepository
    {
        Task<IEnumerable<Branch>> GetAllAsync(CancellationToken cancellationToken);
        Task<Branch?> GetByIdAsync(Guid branchId, CancellationToken cancellationToken);
        Task<IQueryable<Branch>> QueryAsync(CancellationToken cancellationToken);
    }
}