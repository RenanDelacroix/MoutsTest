using MediatR;
using DeveloperStore.Application.DTOs;

namespace DeveloperStore.Application.Queries.Products
{
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public Guid Id { get; }

        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}