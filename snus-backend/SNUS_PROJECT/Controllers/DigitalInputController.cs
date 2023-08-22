using Microsoft.AspNetCore.Mvc;
using SNUS_PROJECT.DTO;
using SNUS_PROJECT.Interfaces;
using SNUS_PROJECT.Models;

namespace SNUS_PROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DigitalInputController : Controller
    {
        private readonly IDigitalInputRepository _DigitalInputRepository;

        public DigitalInputController(IDigitalInputRepository DigitalInputRepository)
        {
            _DigitalInputRepository = DigitalInputRepository;
        }

        [HttpGet("all")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DigitalInput>))]
        public IActionResult GetDigitalInputs()
        {
            var DigitalInputs = _DigitalInputRepository.GetDigitalInputs();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(DigitalInputs);
        }

        [HttpPost("new")]
        public IActionResult AddGetDigitalInput([FromBody] DigitalInputDto DigitalInputDto)
        {
            try
            {
                DigitalInput DigitalInput = new DigitalInput(DigitalInputDto);
                _DigitalInputRepository.AddDigitalInput(DigitalInput);
                return Ok(DigitalInput);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get/{id}")]
        [ProducesResponseType(200, Type = typeof(DigitalInput))]
        [ProducesResponseType(400)]
        public IActionResult GetDigitalInput(int id)
        {
            var DigitalInput = _DigitalInputRepository.GetDigitalInput(id);
            if (DigitalInput.Equals(null))
            {
                return BadRequest("DigitalInput with this id does not exist!");
            }
            else
            {
                return Ok(DigitalInput);
            }
        }
        [HttpGet("getByName/{name}")]
        [ProducesResponseType(200, Type = typeof(DigitalInput))]
        [ProducesResponseType(400)]
        public IActionResult GetDigitalInputByName(string name)
        {
            var DigitalInput = _DigitalInputRepository.GetDigitalInputByName(name);
            if (DigitalInput.Equals(null))
            {
                return BadRequest("DigitalInput with this id does not exist!");
            }
            else
            {
                return Ok(DigitalInput);
            }
        }
        [HttpPost("update/{id}")]
        public IActionResult UpdateDigitalInput([FromBody] DigitalInputDto DigitalInput, int id)
        {
            try
            {
                _DigitalInputRepository.UpdateDigitalInput(DigitalInput, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteDigitalInput(int id)
        {
            try
            {
                _DigitalInputRepository.DeleteDigitalInput(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("turnOnOff/{id}")]
        public IActionResult TurnOnAnalogInput(int id)
        {
            try
            {
                _DigitalInputRepository.TurnOnOffDI(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
