using DeveloperStore.Application.DTOs;
using MediatR;

namespace DeveloperStore.Application.Queries.Sales;

public class GetSalesQuery : IRequest<PagedResult<SaleDto>>
{
    public string? Number { get; set; }
    public string? OrderBy { get; set; } = "CreatedAt";
    public string? Direction { get; set; } = "desc";
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
