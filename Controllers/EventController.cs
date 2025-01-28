using CalendarApp.Models;
using CalendarApp.Services;
using Microsoft.AspNetCore.Mvc;
using CalendarApp.Data;

namespace CalendarApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {

        private readonly CalendarService _calendarService;

        public EventController(CalendarService calendarService)
        {
            _calendarService = calendarService;
        }


        [HttpGet]
        public IActionResult GetEvents()
        {
            var events = _calendarService.GetEvents();

            return Ok(events);
        }

        [HttpPost]
        public IActionResult AddEvent([FromBody] EventDTO eventDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _calendarService.AddEvent(eventDTO);
            return CreatedAtAction(nameof(GetEvents), new { id = eventDTO.Id }, eventDTO);
        }
    }
}