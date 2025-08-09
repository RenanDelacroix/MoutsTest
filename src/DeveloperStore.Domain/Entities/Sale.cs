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
        public Branch Branch { get; set; }
        public string BranchName => Branch.Name;
        public decimal Total => CalculateTotal();

        public Sale()
        {
            Number = GenerateSaleNumber();
        }

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
            CreatedAt = CreatedAt.ToUniversalTime(); // Garantir UTC
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

        private string GenerateSaleNumber()
        {
            // Simples geração randômica de número de venda
            return new Random().Next(1000, 9999).ToString();
        }
    }
}
