using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

/* 
* CREDIT: This code is made by a teacher at UCN.
* I used it as a code along session to get to know the code.
*/


namespace GreetService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HellosController : ControllerBase
    {
        [HttpGet]
        [Produces("application/json")]
        //[ProducesResponseType(StatusCodes.Status200OK)] // Can also add that the type is string.
        public ActionResult<string> Get() {
            //return "hello";
            return Ok("hi");
            //return BadRequest();
        }

        [HttpGet]
        [Route("{name}")]
        ///api/hellos/Hans
        public ActionResult<string> GetName([FromRoute] string name)
        {
            //return "hello";
            return Ok($"hi {name}");
            //return BadRequest();
        }

        [HttpGet]
        [Route("{name}/{times}")]
        ///api/hellos/Hans/7
        public ActionResult<string> GetWhatever([FromRoute] string name, int times)
        {
            //return "hello";
            return Ok($"hi {name} * {times}");
            //return BadRequest();
        }

        [HttpPost]
        public void PostAny([FromBody] string val)
        {
            
        }
    }
}
