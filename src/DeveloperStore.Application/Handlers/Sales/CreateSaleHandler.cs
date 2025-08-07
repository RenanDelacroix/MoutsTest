using AutoMapper;
using DeveloperStore.Application.Commands.Sales;
using DeveloperStore.Application.Events;
using DeveloperStore.Domain.Entities;
using DeveloperStore.Domain.Interfaces;
using MediatR;


namespace DeveloperStore.Application.Handlers.Sales;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, Guid>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = _mapper.Map<Sale>(request);

        sale.ApplyDiscountRules();

        await _saleRepository.AddAsync(sale);

        return sale.Id;
    }
}