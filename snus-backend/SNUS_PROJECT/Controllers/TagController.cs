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
        private readonly IHubContext<AlarmHub> _alarmHubContext;

        public TagController(IAnalogInputRepository analogInputRepository, IDigitalInputRepository digitalInputRepository,
            IHubContext<TagHub> hubContext, IHubContext<AlarmHub> alarmHubContext)
        {
            _analogInputRepository = analogInputRepository;
            _digitalInputRepository = digitalInputRepository;
            _hubContext = hubContext;
            _alarmHubContext = alarmHubContext;
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
                    await _alarmHubContext.Clients.All.SendAsync("ReceiveAlarm", _analogInputRepository.GetAnalogInput(id).Alarms.OrderBy(obj => Math.Abs((obj.TimeStamp - DateTime.Now).TotalMilliseconds))
            .FirstOrDefault());
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
