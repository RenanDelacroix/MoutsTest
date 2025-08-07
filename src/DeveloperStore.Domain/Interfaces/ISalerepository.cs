using DeveloperStore.Domain.Entities;

namespace DeveloperStore.Domain.Interfaces;

public interface ISaleRepository
{
    Task AddAsync(Sale sale, CancellationToken cancellationToken = default);
    Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Sale>> GetAllAsync(CancellationToken cancellationToken = default);
    Task UpdateAsync(Sale sale, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IQueryable<Sale>> QueryAsync(CancellationToken cancellationToken = default);
}