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
    private readonly ILogger<SaleController> _logger;

    public SaleController(IMediator mediator, ILogger<SaleController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSaleCommand command)
    {
        try
        {
            _logger.LogInformation("Received CreateSaleCommand for CustomerId: {CustomerId}", command.CustomerId);
            var saleId = await _mediator.Send(command);
            _logger.LogInformation("Sale created with ID: {SaleId}", saleId);
            return CreatedAtAction(nameof(GetById), new { id = saleId }, new { id = saleId });
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Validation failed during sale creation.");
            return BadRequest($"{ex.Message}");
        }
        
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
            return NotFound(new { message = $"Venda não encontrada." });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
    [FromQuery] long number,
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
            _logger.LogInformation("Attempting to cancel sale with ID: {SaleId}", id);
            await _mediator.Send(new CancelSaleCommand(id));
            _logger.LogInformation("Successfully canceled sale with ID: {SaleId}", id);
            return Ok(new { message = $"Venda cancelada com sucesso." });
        }
        catch (KeyNotFoundException)
        {
            _logger.LogWarning("Sale not found: {SaleId}", id);
            return NotFound(new { message = $"Sale {id} not found." });
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Failed to cancel sale with ID: {SaleId}", id);
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPatch("{id}/pay")]
    public async Task<IActionResult> Pay(Guid id)
    {
        try
        {
            await _mediator.Send(new PaySaleCommand(id));
            return Ok(new { message = $"Venda paga com sucesso." });
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
