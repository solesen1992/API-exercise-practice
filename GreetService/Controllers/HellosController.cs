using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GreetService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HellosController : ControllerBase
    {
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)] // Can also add that the type is string.
        public ActionResult<string> Get() {
            //return "hello";
            //return Ok("hi");
            return BadRequest();
        }
    }
}
