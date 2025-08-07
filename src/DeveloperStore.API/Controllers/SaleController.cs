using DeveloperStore.Application.Commands.Sales;
using DeveloperStore.Application.Queries.Sales;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SaleController : ControllerBase
{
    private readonly IMediator _mediator;

    public SaleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSaleCommand command)
    {
        var saleId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = saleId }, new { id = saleId });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var result = await _mediator.Send(new GetSaleByIdQuery(id));
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = $"Sale with ID {id} not found." });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
    [FromQuery] string? number,
    [FromQuery] string? orderBy = "createdAt",
    [FromQuery] string? direction = "desc",
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10)
    {
        var query = new GetSalesQuery
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

    [HttpPatch("{id}/cancel")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        try
        {
            await _mediator.Send(new CancelSaleCommand(id));
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = $"Sale {id} not found." });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

}
