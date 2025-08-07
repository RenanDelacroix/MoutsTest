using DeveloperStore.Domain.Entities;
using DeveloperStore.Domain.Enums;

namespace DeveloperStore.Unit.Tests.Domain;

public class SaleTests
{
    [Fact]
    public void Cancel_Should_Set_Status_To_Cancelled_When_Status_Is_Created()
    {
        // Arrange
        var sale = new Sale
        {
            Status = SaleStatus.Created
        };

        // Act
        sale.Cancel();

        // Assert
        Assert.Equal(SaleStatus.Cancelled, sale.Status);
    }

    [Theory]
    [InlineData(SaleStatus.Paid)]
    [InlineData(SaleStatus.Cancelled)]
    public void Cancel_Should_Throw_When_Status_Is_Not_Created(SaleStatus status)
    {
        // Arrange
        var sale = new Sale
        {
            Status = status
        };

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => sale.Cancel());
    }

    [Fact]
    public void Total_Should_Calculate_Correctly()
    {
        // Arrange
        var sale = new Sale
        {
            Discount = 10,
            Items = new List<SaleItem>
        {
            new SaleItem { UnitPrice = 50, Quantity = 1, Discount = 0 }, // Subtotal = 50
            new SaleItem { UnitPrice = 30, Quantity = 1, Discount = 0 }  // Subtotal = 30
        }
        };

        // Act
        var total = sale.Total;

        // Assert
        Assert.Equal(70, total); // (50 + 30) - 10
    }

    [Fact]
    public void Discount_Should_Be_10Percent_When_Quantity_Is_Between_4_And_9()
    {
        var item = new SaleItem
        {
            ProductId = Guid.NewGuid(),
            Quantity = 5,
            UnitPrice = 100
        };

        var sale = new Sale
        {
            Items = new List<SaleItem> { item }
        };

        sale.ApplyDiscountRules();

        Assert.Equal(50, item.Discount); // 5 * 100 * 10%
    }

    [Fact]
    public void Discount_Should_Be_20Percent_When_Quantity_Is_Between_10_And_20()
    {
        var item = new SaleItem
        {
            ProductId = Guid.NewGuid(),
            Quantity = 12,
            UnitPrice = 200
        };

        var sale = new Sale
        {
            Items = new List<SaleItem> { item }
        };

        sale.ApplyDiscountRules();

        Assert.Equal(480, item.Discount); // 12 * 200 * 20%
    }

    [Fact]
    public void Should_Throw_When_Quantity_Is_Above_20()
    {
        var item = new SaleItem
        {
            ProductId = Guid.NewGuid(),
            Quantity = 21,
            UnitPrice = 50
        };

        var sale = new Sale
        {
            Items = new List<SaleItem> { item }
        };

        Assert.Throws<InvalidOperationException>(() => sale.ApplyDiscountRules());
    }

    [Fact]
    public void Should_Throw_When_Discount_Is_Given_Below_Minimum_Quantity()
    {
        var item = new SaleItem
        {
            ProductId = Guid.NewGuid(),
            Quantity = 2,
            UnitPrice = 100,
            Discount = 10
        };

        var sale = new Sale
        {
            Items = new List<SaleItem> { item }
        };

        Assert.Throws<InvalidOperationException>(() => sale.ApplyDiscountRules());
    }

    [Fact]
    public void Should_Not_Throw_When_No_Discount_And_Quantity_Is_Below_4()
    {
        var item = new SaleItem
        {
            ProductId = Guid.NewGuid(),
            Quantity = 2,
            UnitPrice = 100,
            Discount = 0
        };

        var sale = new Sale
        {
            Items = new List<SaleItem> { item }
        };

        var ex = Record.Exception(() => sale.ApplyDiscountRules());

        Assert.Null(ex); // deve passar
        Assert.Equal(0, item.Discount); // continua zero
    }
}
