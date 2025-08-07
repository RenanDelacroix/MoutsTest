namespace DeveloperStore.Application.DTOs
{
    public class CreateSaleDto
    {
        public string Number { get; set; } = null!;
        public Guid CustomerId { get; set; }
        public Guid BranchId { get; set; }
        public decimal Discount { get; set; }
        public List<CreateSaleItemDto> Items { get; set; } = new();
    }
}
