using DeveloperStore.Domain.Entities;
using DeveloperStore.Domain.Interfaces;
using DeveloperStore.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DeveloperStore.Infrastructure.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly SalesDbContext _context;

    public SaleRepository(SalesDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        await _context.Sales.AddAsync(sale, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Sales
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<List<Sale>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Sales
            .Include(s => s.Items)
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        _context.Sales.Update(sale);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var sale = await GetByIdAsync(id, cancellationToken);
        if (sale is not null)
        {
            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public Task<IQueryable<Sale>> QueryAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_context.Sales
            .Include(s => s.Items)
            .AsNoTracking()
            .AsQueryable());
    }
}
