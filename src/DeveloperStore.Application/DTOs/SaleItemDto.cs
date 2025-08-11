namespace DeveloperStore.Application.DTOs;

public class SaleItemDto
{
    public long Id { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public ProductDto Product { get; set; }
    public decimal Subtotal => (UnitPrice * Quantity) - Discount;
}
