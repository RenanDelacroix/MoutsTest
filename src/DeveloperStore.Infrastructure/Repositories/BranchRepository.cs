using DeveloperStore.Domain.Entities;
using DeveloperStore.Domain.Interfaces;
using DeveloperStore.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DeveloperStore.Infrastructure.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly SalesDbContext _context;

        public BranchRepository(SalesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Branch>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Branches.ToListAsync(cancellationToken);
        }

        public async Task<Branch?> GetByIdAsync(Guid branchId, CancellationToken cancellationToken)
        {
            return await _context.Branches.FirstOrDefaultAsync(b => b.Id == branchId, cancellationToken);
        }

        public Task<IQueryable<Branch>> QueryAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_context.Branches
                .AsNoTracking()
                .AsQueryable());
        }
    }
}