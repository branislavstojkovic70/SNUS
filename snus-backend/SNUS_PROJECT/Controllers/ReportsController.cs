using Microsoft.AspNetCore.Mvc;
using SNUS_PROJECT.DTO;
using SNUS_PROJECT.Interfaces;
using SNUS_PROJECT.Models;
using System.Reflection.Metadata.Ecma335;

namespace SNUS_PROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : Controller
    {
        private readonly IAlarmRepository _alarmRepository;
        private readonly IAnalogInputRepository _analogInputRepository;
        private readonly IAnalogOutputRepository _analogOutputRepository;
        private readonly IDigitalInputRepository _digitalInputRepository;
        private readonly IDigitalOutputRepository _digitalOutputRepository;
        private readonly IReportRepository _reportRepository;

        public ReportsController(IAlarmRepository alarmRepository, IAnalogInputRepository analogInputRepository, IAnalogOutputRepository analogOutputRepository, IDigitalInputRepository digitalInputRepository, IDigitalOutputRepository digitalOutputRepository, IReportRepository reportRepository)
        {
            _alarmRepository = alarmRepository;
            _analogInputRepository = analogInputRepository;
            _analogOutputRepository = analogOutputRepository;
            _digitalInputRepository = digitalInputRepository;
            _digitalOutputRepository = digitalOutputRepository;
            _reportRepository = reportRepository;
        }
        [HttpGet("FindAllAlarmsByDateRange")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Alarm>))]
        public ActionResult FindAllAlarmsByDateRange([FromQuery] DateTime from, [FromQuery] DateTime to, [FromQuery] int sort) 
        {
            var alarms = _alarmRepository.GetAlarmsInTimePeriod(from, to, sort);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(alarms);
        }

        [HttpGet("FindAllAlarmsByPriority")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Alarm>))]
        public ActionResult FindAllAlarmsByPriority([FromQuery] int priority, [FromQuery] int sortType)
        {
            var alarms = _alarmRepository.GetAlarmsByPriority(priority, sortType);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(alarms);
        }

        [HttpGet("FindAllTagsByDateRange")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TagDto>))]
        public ActionResult FindAllTagsByDateRange([FromQuery] DateTime from, [FromQuery] DateTime to, [FromQuery] int sort)
        {
            var tags = _reportRepository.GetLatestValuesOfTags(sort, from, to);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(tags);
        }

        [HttpGet("FindLastAITags")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TagDto>))]
        public ActionResult FindLastAITags([FromQuery] int sort)
        {
            var tags = _reportRepository.GetLatestValuesOfAITags(sort);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(tags);
        }
        [HttpGet("FindLastDITags")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TagDto>))]
        public ActionResult FindLastDITags([FromQuery] int sort)
        {
            var tags = _reportRepository.GetLatestValuesOfDITags(sort);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(tags);
        }

        [HttpGet("FindTagsByName")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TagDto>))]
        public ActionResult FindTagsByName([FromQuery] int type, [FromQuery] int sort, [FromQuery] string name)
        {
            var tags = GetTagsByTagType(type, sort, name);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(tags);
        }

        private ICollection<TagDto> GetTagsByTagType(int type, int sort, string name)
        {
            switch (type)
            {
                case 0:
                    return _analogInputRepository.GetAnalogInputsById(name, sort);
                case 1:
                    return _analogOutputRepository.GetAnalogOutputsById(name, sort);
                case 2:
                    return _digitalInputRepository.GetDigitalInputsById(name, sort);
                default:
                    return _digitalOutputRepository.GetDigitalOutputsById(name, sort);

            }
        }
    }
}
