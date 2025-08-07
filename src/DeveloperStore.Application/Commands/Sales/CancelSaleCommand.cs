using MediatR;

namespace DeveloperStore.Application.Commands.Sales;

public class CancelSaleCommand : IRequest
{
    public Guid Id { get; }

    public CancelSaleCommand(Guid id)
    {
        Id = id;
    }
}
