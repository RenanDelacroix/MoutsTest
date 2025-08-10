using MediatR;

public class PaySaleCommand : IRequest
{
    public Guid SaleId { get; }

    public PaySaleCommand(Guid saleId)
    {
        SaleId = saleId;
    }
}