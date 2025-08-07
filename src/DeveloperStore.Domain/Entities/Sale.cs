using DeveloperStore.Domain.Enums;

namespace DeveloperStore.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; set; }
        public string Number { get; set; } = null!;
        public Guid CustomerId { get; set; }
        public Guid BranchId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public SaleStatus Status { get; set; } = SaleStatus.Created;
        public decimal Discount { get; set; }
        public List<SaleItem> Items { get; set; } = new();
        public decimal Total => CalculateTotal();

        private decimal CalculateTotal()
        {
            var itemsTotal = Items.Sum(item => item.Subtotal);
            return itemsTotal - Discount;
        }
        public void Cancel()
        {
            if (Status != SaleStatus.Created)
                throw new InvalidOperationException("Only sales in 'Created' status can be cancelled.");

            Status = SaleStatus.Cancelled;
        }

    }
}
