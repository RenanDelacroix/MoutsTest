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
    private readonly IEventPublisher _eventPublisher;

    public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper, IEventPublisher eventPublisher)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _eventPublisher = eventPublisher;
    }

    public async Task<Guid> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = _mapper.Map<Sale>(request);

        sale.ApplyDiscountRules();

        //TODO Persistencia temporária, a real vai ser feita depois
        await _saleRepository.AddAsync(sale);

        await _eventPublisher.PublishAsync(new SaleCreatedEvent
        {
            SaleId = sale.Id,
            Number = sale.Number
        });

        return sale.Id;
    }
}