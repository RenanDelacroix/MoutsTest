using AutoMapper;
using AutoMapper.QueryableExtensions;
using DeveloperStore.Application.DTOs;
using DeveloperStore.Application.Queries.Products;
using DeveloperStore.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeveloperStore.Application.Handlers.Products
{
    public class GetProductsHandler : IRequestHandler<GetProductsQuery, PagedResult<ProductDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductsHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<PagedResult<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var query = await _productRepository.QueryAsync(cancellationToken);

            query = request.OrderBy?.ToLower() switch
            {
                "name" when request.Direction?.ToLower() == "asc" => query.OrderBy(p => p.Name),
                "name" => query.OrderByDescending(p => p.Name),
                "price" when request.Direction?.ToLower() == "asc" => query.OrderBy(p => p.Name),
                "price" => query.OrderByDescending(p => p.Price),
                _ => query.OrderByDescending(p => p.Price)
            };
            var totalCount = await query.CountAsync(cancellationToken);

            var products = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PagedResult<ProductDto>
            {
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                Items = products
            };
        }
    }
}
