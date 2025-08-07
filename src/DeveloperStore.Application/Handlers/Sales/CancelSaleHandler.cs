using DeveloperStore.Application.Commands.Sales;
using DeveloperStore.Domain.Interfaces;
using MediatR;

namespace DeveloperStore.Application.Handlers.Sales;

public class CancelSaleHandler : IRequestHandler<CancelSaleCommand>
{
    private readonly ISaleRepository _repository;

    public CancelSaleHandler(ISaleRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (sale is null)
            throw new KeyNotFoundException($"Sale {request.Id} not found.");

        sale.Cancel(); 

        await _repository.UpdateAsync(sale, cancellationToken);

        return Unit.Value;
    }
}
