using CalendarApp.Models;
using CalendarApp.Services;
using Microsoft.AspNetCore.Mvc;
using CalendarApp.Data;

namespace CalendarApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var events = _userService.GetAllUsers();

            return Ok(events);
        }


        [HttpGet("{id}")]
        public IActionResult GetUserById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid id.");
            }

            var userDTO = _userService.GetUserById(id);
            if (userDTO == null)
            {
                return NotFound();
            }

            return Ok(userDTO);
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _userService.AddUser(userDTO);
            return CreatedAtAction(nameof(GetAllUsers), new { id = userDTO.UserId }, userDTO);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid id.");
            }

            _userService.DeleteUser(id);
            return NoContent();
        }
    }
}
