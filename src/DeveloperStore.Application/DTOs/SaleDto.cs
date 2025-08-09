using DeveloperStore.Application.DTOs;
using DeveloperStore.Domain.Enums;

public class SaleDto
{
    public Guid Id { get; set; }
    public long Number { get; set; } // Changed from string to long
    public Guid CustomerId { get; set; }
    public Guid BranchId { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public SaleStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<SaleItemDto> Items { get; set; } = new();
    public BranchesDto Branch { get; set; } = new();
    public string BranchName { get; set; } = null!;
}
