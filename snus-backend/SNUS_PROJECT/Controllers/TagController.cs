using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SNUS_PROJECT.DTO;
using SNUS_PROJECT.Hubs;
using SNUS_PROJECT.Interfaces;
using SNUS_PROJECT.Models;

namespace SNUS_PROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : Controller
    {
        private readonly IAnalogInputRepository _analogInputRepository;
        private readonly IDigitalInputRepository _digitalInputRepository;
        private readonly IHubContext<TagHub> _hubContext;

        public TagController(IAnalogInputRepository analogInputRepository, IDigitalInputRepository digitalInputRepository,
            IHubContext<TagHub> hubContext)
        {
            _analogInputRepository = analogInputRepository;
            _digitalInputRepository = digitalInputRepository;
            _hubContext = hubContext;
        }

        [HttpPost("changeValue/{id}/{value}/{type}")]
        public async Task<ActionResult<TagDto>> UpdateAnalogInput(int id, int value,int type)
        {
            try
            {
                if(type == 1)
                {
                    AnalogInput ai = _analogInputRepository.ChangeValue(id, value);
                    await _hubContext.Clients.All.SendAsync("ReceiveTag", new TagDto(ai));
                    return Ok();
                }
                else
                {
                    DigitalInput di = _digitalInputRepository.ChangeValue(id, value);
                    await _hubContext.Clients.All.SendAsync("ReceiveTag", new TagDto(di));
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
