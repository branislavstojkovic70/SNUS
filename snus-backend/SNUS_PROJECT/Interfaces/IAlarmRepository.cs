using SNUS_PROJECT.DTO;
using SNUS_PROJECT.Models;

namespace SNUS_PROJECT.Interfaces
{
    public interface IAlarmRepository
    {
        ICollection<Alarm> GetAlarms();
        Alarm GetAlarm(int id);
        bool IsAlarmDeleted(int id);
        Alarm AddAlarm(Alarm alarm);
        void UpdateAlarm(AlarmDto alarm, int id);
        void DeleteAlarm(int id);
        ICollection<Alarm> GetAlarmsInTimePeriod(DateTime from, DateTime to, int sortType);
        ICollection<Alarm> GetAlarmsByPriority(int priority, int sortType);
    }
}
