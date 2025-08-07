namespace DeveloperStore.Domain.Entities
{
    public class SaleItem
    {
        public long Id { get; set; } 
        public Guid ProductId { get; set; }
        public Guid SaleId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }

        public decimal Subtotal => (UnitPrice * Quantity) - Discount;
    }
}
