using AutoMapper;
using AutoMapper.QueryableExtensions;
using DeveloperStore.Application.DTOs;
using DeveloperStore.Application.Queries.Sales;
using DeveloperStore.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeveloperStore.Application.Handlers.Sales;

public class GetSalesHandler : IRequestHandler<GetSalesQuery, PagedResult<SaleDto>>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    public GetSalesHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    public async Task<PagedResult<SaleDto>> Handle(GetSalesQuery request, CancellationToken cancellationToken)
    {
        var query = await _saleRepository.QueryAsync(cancellationToken);

        // Ordenação dinâmica
        query = request.OrderBy?.ToLower() switch
        {
            "createdat" when request.Direction?.ToLower() == "asc" => query.OrderBy(s => s.CreatedAt),
            "createdat" => query.OrderByDescending(s => s.CreatedAt),
            _ => query.OrderByDescending(s => s.CreatedAt)
        };

        var totalCount = await query.CountAsync(cancellationToken);

        var sales = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ProjectTo<SaleDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new PagedResult<SaleDto>
        {
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            Items = sales
        };
    }
}
