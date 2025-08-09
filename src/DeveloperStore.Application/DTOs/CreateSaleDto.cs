namespace DeveloperStore.Application.DTOs
{
    public class CreateSaleDto
    {
        public long Number { get; set; }
        public Guid CustomerId { get; set; }
        public Guid BranchId { get; set; }
        public decimal Discount { get; set; }
        public List<CreateSaleItemDto> Items { get; set; } = new();
    }
}
