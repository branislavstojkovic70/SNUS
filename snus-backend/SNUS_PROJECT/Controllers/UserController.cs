using Microsoft.AspNetCore.Mvc;
using SNUS_PROJECT.Data;
using SNUS_PROJECT.DTO;
using SNUS_PROJECT.Interfaces;
using SNUS_PROJECT.Models;
using SNUS_PROJECT.Repository;
using System.Security.Claims;

namespace SNUS_PROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("new")]
        public IActionResult AddUser([FromBody] UserDto userDto)
        {
            try
            {
                User user = new User(userDto);
                _userRepository.AddUser(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get/{id}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(int id)
        {
            var user = _userRepository.GetUser(id);
            if (user.Equals(null))
            {
                return BadRequest("User with this id does not exist!");
            }
            else
            {
                return Ok(user);
            }
        }
        [HttpGet("getByUsername/{username}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(string username)
        {
            var user = _userRepository.GetUser(username);
            if (user.Equals(null))
            {
                return BadRequest("User with this id does not exist!");
            }
            else
            {
                return Ok(user);
            }
        }
        [HttpPost("login")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult Login([FromBody] UserDto userDto)
        {
            var user = _userRepository.Login(userDto.Username, userDto.Password);
            if (user.Equals(null))
            {
                return BadRequest("Bad credentials!");
            }
            else
            {
                return Ok(user);
            }
        }
    }
}
