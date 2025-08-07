using DeveloperStore.Application.Commands.Sales;
using DeveloperStore.Application.Handlers.Sales;
using DeveloperStore.Domain.Entities;
using DeveloperStore.Domain.Enums;
using DeveloperStore.Domain.Interfaces;
using NSubstitute;

namespace DeveloperStore.Unit.Tests.Application.Handlers;

public class CancelSaleHandlerTests
{
    private readonly ISaleRepository _repository = Substitute.For<ISaleRepository>();

    [Fact]
    public async Task Handle_Should_Cancel_Sale_When_Valid()
    {
        // Arrange
        var sale = new Sale(); 
        var saleId = Guid.NewGuid();

        typeof(Sale).GetProperty(nameof(Sale.Id))!.SetValue(sale, saleId);
        typeof(Sale).GetProperty(nameof(Sale.Status))!.SetValue(sale, SaleStatus.Created);

        var command = new CancelSaleCommand(saleId);
        _repository.GetByIdAsync(saleId, default).Returns(sale);

        var handler = new CancelSaleHandler(_repository);

        // Act
        await handler.Handle(command, default);

        // Assert
        Assert.Equal(SaleStatus.Cancelled, sale.Status);
        await _repository.Received().UpdateAsync(sale, default);
    }

    [Fact]
    public async Task Handle_Should_Throw_When_Sale_Not_Found()
    {
        // Arrange
        var command = new CancelSaleCommand(Guid.NewGuid());
        _repository.GetByIdAsync(command.Id, default).Returns((Sale?)null);

        var handler = new CancelSaleHandler(_repository);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => handler.Handle(command, default));
    }

    [Fact]
    public async Task Handle_Should_Throw_When_Status_Is_Not_Created()
    {
        // Arrange
        var sale = new Sale();
        var saleId = Guid.NewGuid();

        typeof(Sale).GetProperty(nameof(Sale.Id))!.SetValue(sale, saleId);
        typeof(Sale).GetProperty(nameof(Sale.Status))!.SetValue(sale, SaleStatus.Paid);

        var command = new CancelSaleCommand(saleId);
        _repository.GetByIdAsync(saleId, default).Returns(sale);

        var handler = new CancelSaleHandler(_repository);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(command, default));
    }

}
