using DeveloperStore.Domain.Interfaces;
using MediatR;

public class PaySaleHandler : IRequestHandler<PaySaleCommand>
{
    private readonly ISaleRepository _saleRepository;

    public PaySaleHandler(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<Unit> Handle(PaySaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.SaleId, cancellationToken);
        if (sale is null)
            throw new KeyNotFoundException($"Venda não encontrada.");

        sale.Pay();
        await _saleRepository.UpdateAsync(sale, cancellationToken);

        return Unit.Value;
    }
}