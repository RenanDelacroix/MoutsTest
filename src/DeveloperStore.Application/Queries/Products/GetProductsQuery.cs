using DeveloperStore.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperStore.Application.Queries.Products
{
    public class GetProductsQuery : IRequest<PagedResult<ProductDto>>
    {
        public string? Number { get; set; }
        public string? OrderBy { get; set; } = "CreatedAt";
        public string? Direction { get; set; } = "desc";
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
