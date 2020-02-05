using DAL.Payload;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DriverLoginEventsWebService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;

        public EventsController(ILogger<EventsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get(EventPayload payload)
        {
            if (payload == null)
            {
                _logger.LogWarning("Payload is null");
                return BadRequest();
            }
            return new OkResult();
        }
    }
}