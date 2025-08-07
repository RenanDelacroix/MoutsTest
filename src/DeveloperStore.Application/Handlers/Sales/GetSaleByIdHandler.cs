using AutoMapper;
using DeveloperStore.Application.DTOs;
using DeveloperStore.Application.Queries.Sales;
using DeveloperStore.Domain.Interfaces;
using MediatR;

namespace DeveloperStore.Application.Handlers.Sales;

public class GetSaleByIdHandler : IRequestHandler<GetSaleByIdQuery, SaleDto>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    public GetSaleByIdHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    public async Task<SaleDto> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);

        if (sale is null)
            throw new KeyNotFoundException($"Sale with ID {request.Id} not found.");

        return _mapper.Map<SaleDto>(sale);
    }
}
