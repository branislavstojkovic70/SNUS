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
                return Ok();
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


    }
}
