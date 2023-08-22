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
    public class AlarmController : Controller
    {
        private readonly IAlarmRepository _alarmRepository;
        private readonly IAnalogInputRepository _analogInputRepository;
        private readonly IHubContext<AlarmHub> _hubContext;

        public AlarmController(IAlarmRepository alarmRepository, IAnalogInputRepository analogInputRepository, IHubContext<AlarmHub> hubContext)
        {
            _alarmRepository = alarmRepository;
            _analogInputRepository = analogInputRepository;
            _hubContext = hubContext;
        }

        [HttpGet("{id}")]
        public ActionResult<Alarm> GetAlarmActivation(int id)
        {
            // Retrieve the AlarmActivation object by its ID
            var alarm = _alarmRepository.GetAlarm(id);

            if (alarm == null)
            {
                return NotFound();
            }

            return Ok(alarm);
        }

        [HttpGet("all")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Alarm>))]
        public IActionResult GetAlarms()
        {
            var alarms = _alarmRepository.GetAlarms();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(alarms);
        }

        [HttpGet("getByAnalogId/{id}")]
        [ProducesResponseType(200, Type = typeof(List<Alarm>))]
        [ProducesResponseType(400)]
        public IActionResult GetAlarmsByAnalogId(int id)
        {
            var alarms = _alarmRepository.GetAlarms().Where(a => a.AnalogId == id);
            if (alarms.Equals(null))
            {
                return BadRequest("AnalogInput with this id does not exist!");
            }
            else
            {
                return Ok(alarms);
            }
        }

        [HttpPost("new")]
        public async Task<ActionResult<Alarm>> AddAlarm([FromBody] AlarmDto alarm)
        {
            try
            {
                Alarm alarm1 = new Alarm(alarm);
                AnalogInput ai = _analogInputRepository.GetAnalogInput((int)alarm1.AnalogId);
                alarm1.AnalogInput = ai;
                alarm1.MeasureUnit = ai.Units;
                _alarmRepository.AddAlarm(alarm1);
                await _hubContext.Clients.All.SendAsync("ReceiveAlarm", alarm1);
                return Ok(alarm1);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get/{id}")]
        [ProducesResponseType(200, Type = typeof(Alarm))]
        [ProducesResponseType(400)]
        public IActionResult GetAlarm(int id)
        {
            var alarm = _alarmRepository.GetAlarm(id);
            if (alarm.Equals(null))
            {
                return BadRequest("Alarm with this id does not exist!");
            }
            else
            {
                return Ok(alarm);
            }
        }
        [HttpPost("update/{id}")]
        public IActionResult UpdateAlarm([FromBody] AlarmDto alarm, int id)
        {
            try
            {
                _alarmRepository.UpdateAlarm(alarm, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteAlarm(int id)
        {
            try
            {
                _alarmRepository.DeleteAlarm(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
