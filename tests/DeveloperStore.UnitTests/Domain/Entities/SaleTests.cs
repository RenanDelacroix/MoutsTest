using DeveloperStore.Domain.Entities;
using DeveloperStore.Domain.Enums;
using FluentAssertions;

public class SaleTests
{
    [Fact]
    public void Pay_ShouldSetStatusToPaid_WhenStatusIsCreated()
    {
        var sale = SaleTestData.CreateSale();
        sale.Pay();
        sale.Status.Should().Be(SaleStatus.Paid);
    }

    [Fact]
    public void Pay_ShouldThrow_WhenStatusIsNotCreated()
    {
        var sale = new Sale { Status = SaleStatus.Cancelled };
        Action act = () => sale.Pay();
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Cancel_ShouldSetStatusToCancelled_WhenStatusIsCreated()
    {
        var sale = new Sale { Status = SaleStatus.Created };
        sale.Cancel();
        sale.Status.Should().Be(SaleStatus.Cancelled);
    }

    [Fact]
    public void Cancel_ShouldThrow_WhenStatusIsCancelled()
    {
        var sale = new Sale { Status = SaleStatus.Cancelled };
        Action act = () => sale.Cancel();
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void ApplyDiscountRules_ShouldApplyDiscountsCorrectly()
    {
        var sale = new Sale
        {
            Items = new List<SaleItem>
            {
                new SaleItem { Quantity = 5, UnitPrice = 10 }, // 10% 
                new SaleItem { Quantity = 12, UnitPrice = 20 } // 20% 
            }
        };
        sale.ApplyDiscountRules();
        sale.Discount.Should().Be(5 * 10 * 0.10m + 12 * 20 * 0.20m);
    }

    [Fact]
    public void ApplyDiscountRules_ShouldThrow_WhenQuantityExceedsLimit()
    {
        var sale = new Sale
        {
            Items = new List<SaleItem>
            {
                new SaleItem { Quantity = 21, UnitPrice = 10 }
            }
        };
        Action act = () => sale.ApplyDiscountRules();
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Total_ShouldCalculateCorrectly()
    {
        var sale = new Sale
        {
            Items = new List<SaleItem>
            {
                new SaleItem { Quantity = 5, UnitPrice = 10, Discount = 5 },
                new SaleItem { Quantity = 2, UnitPrice = 20, Discount = 0 }
            },
            Discount = 5
        };
        var expectedTotal = (5 * 10 - 5) + (2 * 20 - 0) - 5;
        sale.Total.Should().Be(expectedTotal);
    }
}

public static class SaleTestData
{
    public static Sale CreateSale(SaleStatus status = SaleStatus.Created)
    {
        return new Sale
        {
            Status = status,
            Items = new List<SaleItem>
            {
                new SaleItem { Quantity = 5, UnitPrice = 10 },
                new SaleItem { Quantity = 12, UnitPrice = 20 }
            }
        };
    }
}