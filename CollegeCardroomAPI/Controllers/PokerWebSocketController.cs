using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CollegeCardroomAPI.Services;

namespace CollegeCardroomAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokerController : ControllerBase
    {
        private readonly PokerWebSocketService webSocketService;

        public PokerController(PokerWebSocketService webSocketService)
        {
            this.webSocketService = webSocketService;
        }

        [HttpGet("connect")]
        public async Task Connect()
        {
            await webSocketService.HandleWebSocketConnection(HttpContext);
        }
    }
}