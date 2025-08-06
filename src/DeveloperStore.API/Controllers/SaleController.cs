using Microsoft.AspNetCore.Mvc;

namespace DeveloperStore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SaleController : Controller
    {
        [HttpGet(Name = "Sales")]
        public IActionResult Index()
        {
            return Ok(new {Result = "1, 2, 3"});
        }
    }
}
