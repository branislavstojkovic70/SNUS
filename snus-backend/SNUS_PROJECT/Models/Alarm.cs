using SNUS_PROJECT.DTO;

namespace SNUS_PROJECT.Models
{
    public class Alarm
    {
        public int Id { get; set; }

        public int ThreshHold { get; set; }
        public string Message { get; set; }
        public AnalogInput AnalogInput { get; set; }

        public int? AnalogId { get; set; }

        public DateTime TimeStamp { get; set; }
        public int Priority { get; set; }
        public string Type { get; set; }

        public string? MeasureUnit { get; set; }
        public bool IsDeleted { get; set; }
        public Alarm()
        {

        }

        public Alarm(int threshHold, string message, AnalogInput analogInput, int? analogId, DateTime timeStamp, int priority, string type, string? measureUnit, bool isDeleted)
        {
            ThreshHold = threshHold;
            Message = message;
            AnalogInput = analogInput;
            AnalogId = analogId;
            TimeStamp = timeStamp;
            Priority = priority;
            Type = type;
            MeasureUnit = measureUnit;
            IsDeleted = isDeleted;
        }

        public Alarm(AlarmDto alarmDto)
        {
            ThreshHold = alarmDto.ThreshHold;
            Message = alarmDto.Message;
            AnalogInput = new AnalogInput();
            AnalogId = alarmDto.AnalogId;
            TimeStamp = DateTime.Now;
            Priority = alarmDto.Priority;
            Type = alarmDto.Type;
            //MeasureUnit = alarmDto.U;
            IsDeleted = false;
        }
    }
}
