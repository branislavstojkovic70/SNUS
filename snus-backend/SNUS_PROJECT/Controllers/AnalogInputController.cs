using Microsoft.AspNetCore.Mvc;
using SNUS_PROJECT.DTO;
using SNUS_PROJECT.Interfaces;
using SNUS_PROJECT.Models;
using SNUS_PROJECT.Repository;

namespace SNUS_PROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalogInputController : Controller
    {
        private readonly IAnalogInputRepository _analogInputRepository;

        public AnalogInputController(IAnalogInputRepository analogInputRepository)
        {
            _analogInputRepository = analogInputRepository;
        }

        [HttpGet("all")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AnalogInput>))]
        public IActionResult GetAnalogInputs()
        {
            var AnalogInputs = _analogInputRepository.GetAnalogInputs();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(AnalogInputs);
        }

        [HttpPost("new")]
        public IActionResult AddGetAnalogInput([FromBody] AnalogInputDto analogInputDto)
        {
            try
            {
                AnalogInput analogInput = new AnalogInput(analogInputDto);
                _analogInputRepository.AddAnalogInput(analogInput);
                return Ok(analogInput);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get/{id}")]
        [ProducesResponseType(200, Type = typeof(AnalogInput))]
        [ProducesResponseType(400)]
        public IActionResult GetAnalogInput(int id)
        {
            var AnalogInput = _analogInputRepository.GetAnalogInput(id);
            if (AnalogInput.Equals(null))
            {
                return BadRequest("AnalogInput with this id does not exist!");
            }
            else
            {
                return Ok(AnalogInput);
            }
        }

        [HttpGet("getByName/{name}")]
        [ProducesResponseType(200, Type = typeof(AnalogInput))]
        [ProducesResponseType(400)]
        public IActionResult GetAnalogInputByName(string name)
        {
            var AnalogInput = _analogInputRepository.GetAnalogInputByName(name);
            if (AnalogInput.Equals(null))
            {
                return BadRequest("AnalogInput with this name does not exist!");
            }
            else
            {
                return Ok(AnalogInput);
            }
        }
        [HttpGet("getAllByName/{name}/{sort}")]
        [ProducesResponseType(200, Type = typeof(AnalogInput))]
        [ProducesResponseType(400)]
        public IActionResult GetAnalogInputsByName(string name, int sort)
        {
            var AnalogInput = _analogInputRepository.GetAnalogInputsByName(name, sort);
            if (AnalogInput.Equals(null))
            {
                return BadRequest("AnalogInput with this name does not exist!");
            }
            else
            {
                return Ok(AnalogInput);
            }
        }
        [HttpPost("update/{id}")]
        public IActionResult UpdateAnalogInput([FromBody] AnalogInputDto AnalogInput, int id)
        {
            try
            {
                _analogInputRepository.UpdateAnalogInput(AnalogInput, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteAnalogInput(int id)
        {
            try
            {
                _analogInputRepository.DeleteAnalogInput(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("turnOnOff/{id}")]
        public IActionResult TurnOffAnalogInput(int id)
        {
            try
            {
                bool isActive = _analogInputRepository.TurnOnOffAI(id);
                return Ok(isActive);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
