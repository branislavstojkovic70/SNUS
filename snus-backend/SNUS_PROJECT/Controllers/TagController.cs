using Microsoft.AspNetCore.Mvc;
using SNUS_PROJECT.DTO;
using SNUS_PROJECT.Interfaces;

namespace SNUS_PROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : Controller
    {
        private readonly IAnalogInputRepository _analogInputRepository;
        private readonly IDigitalInputRepository _digitalInputRepository;

        public TagController(IAnalogInputRepository analogInputRepository, IDigitalInputRepository digitalInputRepository)
        {
            _analogInputRepository = analogInputRepository;
            _digitalInputRepository = digitalInputRepository;
        }

        [HttpPost("changeValue/{id}/{value}/{type}")]
        public IActionResult UpdateAnalogInput(int id, int value,int type)
        {
            try
            {
                if(type == 1)
                {
                    _analogInputRepository.ChangeValue(id, value);
                    return Ok();
                }
                else
                {
                    _digitalInputRepository.ChangeValue(id, value);
                    return Ok();
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
