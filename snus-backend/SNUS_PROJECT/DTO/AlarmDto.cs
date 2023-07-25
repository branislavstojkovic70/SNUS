using SNUS_PROJECT.Models;

namespace SNUS_PROJECT.DTO
{
    public class AlarmDto
    {
        public int ThreshHold { get; set; }
        public string Message { get; set; }
        public int? AnalogId { get; set; }
        public int Priority { get; set; }
        public string Type { get; set; }
        public DateTime DateTime { get; set; }

        public AlarmDto() { }

        public AlarmDto(int threshHold, string message, int? analogId, int priority, string type)
        {
            ThreshHold = threshHold;
            Message = message;
            AnalogId = analogId;
            Priority = priority;
            Type = type;
            DateTime = DateTime.Now;    
        }

        public AlarmDto(Alarm alarm)
        {
            ThreshHold = alarm.ThreshHold;
            Message = alarm.Message;
            AnalogId = alarm.AnalogId;
            Priority = alarm.Priority;
            Type = alarm.Type;
            DateTime = DateTime.Now;
        }
    }
}
