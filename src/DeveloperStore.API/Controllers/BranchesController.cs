using DeveloperStore.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BranchesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var branches = ListBranches();

            return Ok(branches);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var branches = ListBranches();
            if (id == Guid.Empty || !branches.Any(x => x.Id == id))
            {
                return BadRequest("Branch not found");
            }

            return Ok(branches.First(x => x.Id == id));
        }

        private static List<BranchesDto> ListBranches()
        {
            return new List<BranchesDto>
            {
                new BranchesDto
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "São Paulo"
                },
                new BranchesDto
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Name = "Santa Catarina"
                }
            };

        }
    }
}
