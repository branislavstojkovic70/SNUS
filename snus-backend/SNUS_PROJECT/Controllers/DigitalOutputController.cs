using Microsoft.AspNetCore.Mvc;
using SNUS_PROJECT.DTO;
using SNUS_PROJECT.Interfaces;
using SNUS_PROJECT.Models;

namespace SNUS_PROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DigitalOutputController : Controller
    {
        private readonly IDigitalOutputRepository _DigitalOutputRepository;

        public DigitalOutputController(IDigitalOutputRepository DigitalOutputRepository)
        {
            _DigitalOutputRepository = DigitalOutputRepository;
        }

        [HttpGet("all")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DigitalOutput>))]
        public IActionResult GetDigitalOutputs()
        {
            var DigitalOutputs = _DigitalOutputRepository.GetDigitalOutputs();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(DigitalOutputs);
        }

        [HttpPost("new")]
        public IActionResult AddGetDigitalOutput([FromBody] DigitalOutputDto DigitalOutputDto)
        {
            try
            {
                DigitalOutput DigitalOutput = new DigitalOutput(DigitalOutputDto);
                _DigitalOutputRepository.AddDigitalOutput(DigitalOutput);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get/{id}")]
        [ProducesResponseType(200, Type = typeof(DigitalOutput))]
        [ProducesResponseType(400)]
        public IActionResult GetDigitalOutput(int id)
        {
            var DigitalOutput = _DigitalOutputRepository.GetDigitalOutput(id);
            if (DigitalOutput.Equals(null))
            {
                return BadRequest("DigitalOutput with this id does not exist!");
            }
            else
            {
                return Ok(DigitalOutput);
            }
        }
        [HttpPost("update/{id}")]
        public IActionResult UpdateDigitalOutput([FromBody] DigitalOutputDto DigitalOutput, int id)
        {
            try
            {
                _DigitalOutputRepository.UpdateDigitalOutput(DigitalOutput, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteDigitalOutput(int id)
        {
            try
            {
                _DigitalOutputRepository.DeleteDigitalOutput(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
