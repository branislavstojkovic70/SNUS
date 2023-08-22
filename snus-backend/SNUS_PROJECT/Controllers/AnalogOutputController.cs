using Microsoft.AspNetCore.Mvc;
using SNUS_PROJECT.DTO;
using SNUS_PROJECT.Interfaces;
using SNUS_PROJECT.Models;

namespace SNUS_PROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalogOutputController : Controller
    {
        private readonly IAnalogOutputRepository _analogOutputRepository;

        public AnalogOutputController(IAnalogOutputRepository analogOutputRepository)
        {
            _analogOutputRepository = analogOutputRepository;
        }

        [HttpGet("all")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AnalogOutput>))]
        public IActionResult GetAnalogOutputs()
        {
            var AnalogOutputs = _analogOutputRepository.GetAnalogOutputs();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(AnalogOutputs);
        }

        [HttpPost("new")]
        public IActionResult AddGetAnalogOutput([FromBody] AnalogOutputDto analogOutputDto)
        {
            try
            {
                AnalogOutput analogOutput = new AnalogOutput(analogOutputDto);
                _analogOutputRepository.AddAnalogOutput(analogOutput);
                return Ok(analogOutput);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get/{id}")]
        [ProducesResponseType(200, Type = typeof(AnalogOutput))]
        [ProducesResponseType(400)]
        public IActionResult GetAnalogOutput(int id)
        {
            var AnalogOutput = _analogOutputRepository.GetAnalogOutput(id);
            if (AnalogOutput.Equals(null))
            {
                return BadRequest("AnalogOutput with this id does not exist!");
            }
            else
            {
                return Ok(AnalogOutput);
            }
        }

        [HttpGet("getByName/{name}")]
        [ProducesResponseType(200, Type = typeof(AnalogOutput))]
        [ProducesResponseType(400)]
        public IActionResult GetAnalogOutputByName(string name)
        {
            var AnalogOutput = _analogOutputRepository.GetAnalogOutputByName(name);
            if (AnalogOutput.Equals(null))
            {
                return BadRequest("AnalogOutput with this id does not exist!");
            }
            else
            {
                return Ok(AnalogOutput);
            }
        }
        [HttpPost("update/{id}")]
        public IActionResult UpdateAnalogOutput([FromBody] AnalogOutputDto AnalogOutput, int id)
        {
            try
            {
                _analogOutputRepository.UpdateAnalogOutput(AnalogOutput, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteAnalogOutput(int id)
        {
            try
            {
                _analogOutputRepository.DeleteAnalogOutput(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
