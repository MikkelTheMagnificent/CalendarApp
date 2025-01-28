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


        [HttpGet("{id}")]
        public IActionResult GetEventById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid id.");
            }

            var eventDTO = _calendarService.GetEvent(id);
            if (eventDTO == null)
            {
                return NotFound();
            }

            return Ok(eventDTO);
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


        [HttpPut("{id}")]
        public IActionResult UpdateEvent(string id, [FromBody] EventDTO eventDTO)
        {
            if (eventDTO == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _calendarService.UpdateEvent(id, eventDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid id.");
            }

            _calendarService.DeleteEvent(id);
            return NoContent();
        }

    }
}