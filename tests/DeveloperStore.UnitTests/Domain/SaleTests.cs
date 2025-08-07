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
}
