namespace SNUS_PROJECT.Models
{
    public class AlarmActivation
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }

        public Alarm Alarm { get; set; }
        public int? AlarmId { get; set; }

        public AlarmActivation(DateTime timestamp, Alarm alarm, int? alarmId)
        {
            Timestamp = timestamp;
            Alarm = alarm;
            AlarmId = alarmId;
        }

        public AlarmActivation()
        {
        }
    }
}
