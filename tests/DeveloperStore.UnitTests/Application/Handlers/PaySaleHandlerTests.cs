using Moq;
using DeveloperStore.Domain.Entities;
using DeveloperStore.Domain.Enums;
using DeveloperStore.Domain.Interfaces;
using FluentAssertions;

public class PaySaleHandlerTests
{
    [Fact]
    public async Task Handle_ShouldSetStatusToPaid_WhenSaleIsCreated()
    {
        var sale = new Sale { Status = SaleStatus.Created };
        var repoMock = new Mock<ISaleRepository>();
        repoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(sale);

        var handler = new PaySaleHandler(repoMock.Object);
        await handler.Handle(new PaySaleCommand(Guid.NewGuid()), CancellationToken.None);

        sale.Status.Should().Be(SaleStatus.Paid);
        repoMock.Verify(r => r.UpdateAsync(sale, It.IsAny<CancellationToken>()), Times.Once);
    }
}