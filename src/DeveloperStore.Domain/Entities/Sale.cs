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

        public void ApplyDiscountRules()
        {
            foreach (var item in Items)
            {
                // Validação: limite máximo por produto
                if (item.Quantity > 20)
                    throw new InvalidOperationException($"Product {item.ProductId} exceeds the maximum allowed quantity (20).");

                // Regra: sem desconto abaixo de 4
                if (item.Quantity < 4)
                {
                    if (item.Discount > 0)
                        throw new InvalidOperationException($"Product {item.ProductId} cannot have a discount with less than 4 items.");

                    continue;
                }

                // Regra de desconto baseado na quantidade
                var calculatedDiscount = item.UnitPrice * item.Quantity * (
                    item.Quantity < 10 ? 0.10m : 0.20m
                );

                item.Discount = calculatedDiscount;
            }
        }


    }
}
