using DeveloperStore.Domain.Entities;
using DeveloperStore.Domain.Interfaces;
using DeveloperStore.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace DeveloperStore.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly SalesDbContext _context;

        public ProductRepository(SalesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid productId, CancellationToken cancellationToken)
        {
            return await _context.Products.FirstOrDefaultAsync(s => s.Id == productId, cancellationToken);
        }
    }
}
