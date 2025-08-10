using DeveloperStore.Domain.Enums;

namespace DeveloperStore.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; set; }
        public long Number { get; set; } 
        public Guid CustomerId { get; set; }
        public Guid BranchId { get; set; }
        public DateTime CreatedAt { get; set; }
        public SaleStatus Status { get; set; } = SaleStatus.Created;
        public decimal Discount { get; set; }
        public List<SaleItem> Items { get; set; } = new();
        public Branch Branch { get; set; }
        public string BranchName => Branch.Name;
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
            Discount = 0;

            foreach (var item in Items)
            {
                // Validação: limite máximo por produto
                if (item.Quantity > 20)
                    throw new InvalidOperationException($"Produto excedeu a quantidade máxima permitida (20).");

                // Validação: desconto não permitido para menos de 4 unidades
                if (item.Quantity < 4)
                {
                    item.Discount = 0;
                    continue;
                }

                // Cálculo do desconto
                var percentage = item.Quantity < 10 ? 0.10m : 0.20m;
                item.Discount = item.UnitPrice * item.Quantity * percentage;
                Discount += item.Discount;
            }
        }

        public void Pay()
        {
            if (Status != SaleStatus.Created)
                throw new InvalidOperationException("Apenas vendas em status Created podem ser pagas.");

            Status = SaleStatus.Paid;
        }
    }
}
