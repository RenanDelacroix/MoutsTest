using AutoMapper;
using DeveloperStore.Application.Queries.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public ProductController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? number,
    [FromQuery] string? orderBy = "name",
    [FromQuery] string? direction = "desc",
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10)
    {
        var query = new GetProductsQuery
        {
            Number = number,
            OrderBy = orderBy,
            Direction = direction,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = $"Sale with ID {id} not found." });
        }
    }
}
