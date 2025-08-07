using DeveloperStore.Application.DTOs;
using MediatR;

namespace DeveloperStore.Application.Queries.Sales;

public class GetSaleByIdQuery : IRequest<SaleDto>
{
    public Guid Id { get; set; }

    public GetSaleByIdQuery(Guid id)
    {
        Id = id;
    }
}