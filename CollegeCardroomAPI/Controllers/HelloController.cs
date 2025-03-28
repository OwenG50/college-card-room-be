using CollegeCardroomAPI.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CollegeCardroomAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HelloController : ControllerBase
    {
        private readonly IHelloManager helloManager;

        public HelloController(IHelloManager helloManager)
        {
            this.helloManager = helloManager;
        }

        [HttpGet]
        public IActionResult GetHello()
        {
            var message = helloManager.GetHello();
            return Ok(message);
        }
    }

}
