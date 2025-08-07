using DeveloperStore.Application.DTOs;
using MediatR;

public class CreateSaleCommand : IRequest<Guid>
{
    public Guid CustomerId { get; set; }
    public Guid BranchId { get; set; }
    public List<CreateSaleItemDto> Items { get; set; } = new();
}
