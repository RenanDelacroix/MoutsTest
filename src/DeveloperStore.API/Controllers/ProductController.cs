using AutoMapper;
using DeveloperStore.API.DTOs;
using DeveloperStore.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public ProductController(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var products = await _repository.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
    }
}
