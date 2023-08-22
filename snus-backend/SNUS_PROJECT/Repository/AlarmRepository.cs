using Microsoft.EntityFrameworkCore;
using SNUS_PROJECT.Data;
using SNUS_PROJECT.DTO;
using SNUS_PROJECT.Interfaces;
using SNUS_PROJECT.Models;
using System.Security.Claims;

namespace SNUS_PROJECT.Repository
{
    public class AlarmRepository : IAlarmRepository
    {
        private DataContext _dataContext;
        public AlarmRepository(DataContext dataContext)
        { 
            _dataContext = dataContext;
        }

        public ICollection<Alarm> GetAlarms()
        {
            return _dataContext.Alarms.Where(a => a.IsDeleted == false).OrderBy(p => p.Id).ToList();
        }

        public Alarm GetAlarm(int id)
        {
            return _dataContext.Alarms.First(u => u.Id == id);
        }

        public Alarm AddAlarm(Alarm alarm)
        {
            _dataContext.Alarms.Add(alarm);
            AnalogInput ai = _dataContext.AnalogInputs.Where(p => p.Id == alarm.AnalogId).FirstOrDefault();
            ai.Alarms.Add(alarm);
            _dataContext.SaveChanges();
            Console.WriteLine(alarm.Id);
            return alarm;
        }


        public bool IsAlarmDeleted(int id)
        {
            var alarm = _dataContext.Alarms.Where(p => p.Id == id).FirstOrDefault();
            if (alarm != null) 
            {
                return alarm.IsDeleted;
            }
            else 
            { 
                return false; 
            }
        }

        public void UpdateAlarm(AlarmDto alarm, int id)
        {
            var existingAlarm = _dataContext.Alarms.Where(p => p.Id == id).FirstOrDefault();
            if (existingAlarm != null)
            {
                existingAlarm.ThreshHold = alarm.ThreshHold;
                existingAlarm.Message = alarm.Message;
                existingAlarm.Priority = alarm.Priority;
                existingAlarm.TimeStamp = alarm.DateTime;
                _dataContext.SaveChanges();
            }
        }

        public void DeleteAlarm(int id)
        {
            var alarmToRemove = _dataContext.Alarms.Where(p => p.Id == id).FirstOrDefault();

            if (alarmToRemove != null)
            {
                AnalogInput ai = _dataContext.AnalogInputs.Where(p => p.Id == alarmToRemove.AnalogId).FirstOrDefault();
                ai.Alarms.Remove(alarmToRemove);
                alarmToRemove.IsDeleted = true;
                _dataContext.SaveChanges();
            }
        }

        public ICollection<Alarm> GetAlarmsInTimePeriod(DateTime from, DateTime to, int sortType)

        {
            var als = new List<AlarmActivation>();
            if (sortType == 1)
            {
                als = _dataContext.AlarmActivations.Where(alarm => alarm.Timestamp >= from && alarm.Timestamp <= to).OrderBy(alarm => alarm.Timestamp).ToList();
            }
            else if (sortType == 2)
            {
                als = _dataContext.AlarmActivations.Where(alarm => alarm.Timestamp >= from && alarm.Timestamp <= to).OrderBy(alarm => alarm.Alarm.Priority).ToList();
            }
            else if (sortType == 3)
            {
                als = _dataContext.AlarmActivations.Where(alarm => alarm.Timestamp >= from && alarm.Timestamp <= to).OrderByDescending(alarm => alarm.Timestamp).ToList();
            }
            else
            {
                als = _dataContext.AlarmActivations.Where(alarm => alarm.Timestamp >= from && alarm.Timestamp <= to).OrderByDescending(alarm => alarm.Alarm.Priority).ToList();
            }
            List<Alarm> alarms = new List<Alarm>();
            foreach (AlarmActivation aa in als)
            {
                if (aa.AlarmId!=null)
                {
                    alarms.Add(_dataContext.Alarms.Find(aa.AlarmId));
                }
            }
            return alarms;
        }

        public ICollection<Alarm> GetAlarmsByPriority(int priority, int sortType)
        {
            var alarms = new List<Alarm>();
            if (sortType == 1)
            {
                alarms = _dataContext.Alarms.Where(alarm => alarm.Priority == priority).OrderBy(alarm => alarm.TimeStamp).ToList();

            }
            else
            {
                alarms = _dataContext.Alarms.Where(alarm => alarm.Priority == priority).OrderByDescending(alarm => alarm.TimeStamp).ToList();
            }
            foreach (Alarm alarm in alarms)
            {
                alarm.AnalogInput =  _dataContext.AnalogInputs.Find(alarm.AnalogId);
            }
            return alarms;
        }

    }
}
