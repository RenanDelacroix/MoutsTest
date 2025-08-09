using AutoMapper;
using DeveloperStore.Application.Queries.Branches;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BranchesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public BranchesController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetBranchesQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var result = await _mediator.Send(new GetBranchByIdQuery(id));
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = $"Branch with ID {id} not found." });
        }
    }
}