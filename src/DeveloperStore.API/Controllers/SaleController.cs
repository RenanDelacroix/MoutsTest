using DeveloperStore.Application.Commands.Sales;
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

    /// <summary>
    /// Cria uma nova venda
    /// </summary>
    /// <param name="command">Dados da venda</param>
    /// <returns>Id da venda criada</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSaleCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var saleId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = saleId }, new { id = saleId });
    }

    // Stub para retorno de CreatedAtAction
    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        return Ok(new { Result = "1, 2, 3"}); //TODO implementar lógica para buscar a venda por ID depois
    }
}
