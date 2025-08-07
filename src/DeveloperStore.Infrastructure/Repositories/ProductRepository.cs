using DeveloperStore.Domain.Entities;
using DeveloperStore.Domain.Interfaces;
using DeveloperStore.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

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
    }
}
