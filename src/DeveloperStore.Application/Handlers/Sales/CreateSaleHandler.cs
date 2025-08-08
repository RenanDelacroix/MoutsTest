using AutoMapper;
using DeveloperStore.Application.Commands.Sales;
using DeveloperStore.Application.Events;
using DeveloperStore.Domain.Entities;
using DeveloperStore.Domain.Interfaces;
using MediatR;

namespace DeveloperStore.Application.Handlers.Sales;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, Guid>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IBranchRepository _branchRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper, IProductRepository productRepository, IBranchRepository branchRepository)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _productRepository = productRepository;
        _branchRepository = branchRepository;
    }

    public async Task<Guid> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = new Sale
        {
            CustomerId = request.CustomerId,
            BranchId = request.BranchId,
            Items = new List<SaleItem>()
        };

        foreach (var itemDto in request.Items)
        {
            var product = await _productRepository.GetByIdAsync(itemDto.ProductId, cancellationToken);
            if (product == null)
                throw new InvalidOperationException($"Product {itemDto.ProductId} not found.");

            sale.Items.Add(new SaleItem
            {
                ProductId = itemDto.ProductId,
                Quantity = itemDto.Quantity,
                UnitPrice = product.Price 
            });
        }

        sale.ApplyDiscountRules();

        await _saleRepository.AddAsync(sale);

        return sale.Id;
    }
}
