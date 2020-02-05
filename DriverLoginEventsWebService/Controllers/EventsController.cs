using DAL;
using DAL.Models;
using DAL.Payload;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;

namespace DriverLoginEventsWebService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        #region Fields

        private readonly ILogger<EventsController> _logger;

        private readonly IDriversRepository _repository;

        #endregion

        public EventsController(IDriversRepository repository, ILogger<EventsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post(EventPayload payload)
        {
            if (payload == null)
            {
                _logger.LogError("Payload is null");
                return BadRequest();
            }

            try
            {
                var driver = GetDriver(payload.DriverId);
                if (driver == null)
                {
                    _logger.LogError($"Not found driver with id: { payload.DriverId }");
                    return BadRequest();
                }

                AddLoginEvent(driver.Id, payload.EventTimestamp);

                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }

        #region Private

        private Driver GetDriver(int driverId)
        {
            return _repository.GetDrivers().Where(d => d.Id == driverId).FirstOrDefault();           
        }

        private void AddLoginEvent(int driverId, DateTime time)
        {
            var loginEvent = new Event() 
            {
                DriverId = driverId,
                Time = time,
            };

            _repository.AddEvent(loginEvent);
        }

        #endregion
    }
}