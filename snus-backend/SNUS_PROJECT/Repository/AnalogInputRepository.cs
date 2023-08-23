using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using SNUS_PROJECT.Data;
using SNUS_PROJECT.DTO;
using SNUS_PROJECT.Hubs;
using SNUS_PROJECT.Interfaces;
using SNUS_PROJECT.Models;
using System.Text.Json;

namespace SNUS_PROJECT.Repository
{
    public class AnalogInputRepository : IAnalogInputRepository
    {
        private DataContext _dataContext;
        public AnalogInputRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public void AddAnalogInput(AnalogInput analogInput)
        {
            _dataContext.AnalogInputs.Add(analogInput);
            _dataContext.SaveChanges();
            CheckAlarm((int)analogInput.Value, analogInput);
        }

        public void DeleteAnalogInput(int id)
        {
            var aiToRemove = _dataContext.AnalogInputs.Where(p => p.Id == id).FirstOrDefault();

            if (aiToRemove != null)
            {
                _dataContext.AnalogInputs.Remove(aiToRemove);
                _dataContext.SaveChanges();
            }
        }

        public AnalogInput GetAnalogInput(int id)
        {
            return _dataContext.AnalogInputs.First(u => u.Id == id);
        }
        public AnalogInput GetAnalogInputByName(string name)
        {
            return _dataContext.AnalogInputs.First(u => u.Name == name);
        }

        public ICollection<AnalogInput> GetAnalogInputs()
        {
            return _dataContext.AnalogInputs.OrderBy(p => p.Id).ToList();
        }
        public ICollection<TagDto> GetAnalogInputsById(string name, int sort)
        {
            List<TagDto> result = new List<TagDto>();
            if (sort == 0)
            {
                List<AnalogInput> ais = _dataContext.AnalogInputs.Where(p => p.Name == name).OrderBy(ai => ai.Value).ToList();
                foreach (var ai in ais)
                {
                    result.Add(new TagDto(ai));
                }
            }
            else
            {
                List<AnalogInput> ais = _dataContext.AnalogInputs.Where(p => p.Name == name).OrderByDescending(ai => ai.Value).ToList();
                foreach (var ai in ais)
                {
                    result.Add(new TagDto(ai));
                }
            }
            return result;
        }
        public List<AnalogInput> GetAnalogInputsByName(string name, int sort)
        {
            List<AnalogInput> ais = new List<AnalogInput>();
            if (sort == 0)
            {
                 ais = _dataContext.AnalogInputs.Where(p => p.Name==name).OrderBy(ai => ai.Value).ToList();
            }
            else
            {
                ais = _dataContext.AnalogInputs.Where(p => p.Name == name).OrderByDescending(ai => ai.Value).ToList();
            }
            return ais;
        }

        public bool? IsAnalogInputActive(int id)
        {
            var analogInput = _dataContext.AnalogInputs.Where(p => p.Id == id).FirstOrDefault();
            if (!analogInput.Equals(null))
            {
                return analogInput.IsActive;
            }
            else
            {
                return false;
            }
        }

        public void UpdateAnalogInput(AnalogInputDto analogInputDto, int id)
        {
            var existingAnalogInput = _dataContext.AnalogInputs.Where(p => p.Id == id).FirstOrDefault();
            if (existingAnalogInput != null)
            {
                existingAnalogInput.Name = analogInputDto.Name;
                existingAnalogInput.Description = analogInputDto.Description;
                existingAnalogInput.IOAddress = analogInputDto.IOAddress;
                existingAnalogInput.Driver = analogInputDto.Driver;
                existingAnalogInput.ScanTime = analogInputDto.ScanTime;
                existingAnalogInput.LowLimit = analogInputDto.LowLimit;
                existingAnalogInput.HighLimit = analogInputDto.HighLimit;
                existingAnalogInput.Units = analogInputDto.Units;
                existingAnalogInput.Value = analogInputDto.Value;
                CheckAlarm((int)existingAnalogInput.Value, existingAnalogInput);
                _dataContext.SaveChanges();
            }
        }

        public IEnumerable<AnalogInput> GetLatestAnalogInputsPerIOAddress()
        {
            var latestInputs = _dataContext.AnalogInputs.Where(a => a.IsActive == true)
                .GroupBy(a => a.IOAddress)
                .Select(g => g.OrderByDescending(a => a.DateTime).FirstOrDefault());

            return latestInputs;
        }

        
        public bool TurnOnOffAI(int id)
        {
            var existingAnalogInput = _dataContext.AnalogInputs.Where(p => p.Id == id).FirstOrDefault();
            if (existingAnalogInput != null)
            {
                existingAnalogInput.IsActive = !existingAnalogInput.IsActive;
                _dataContext.SaveChanges();
                return (bool)existingAnalogInput.IsActive;
            }
            return false;
        }

        public AnalogInput? ChangeValue(int id, int value)
        {
            var existingAnalogInput = _dataContext.AnalogInputs.Where(p => p.Id == id).FirstOrDefault();
            if (existingAnalogInput != null)
            {
                existingAnalogInput.Value = value;
                _dataContext.SaveChanges();
                CheckAlarm(value, existingAnalogInput);
                return existingAnalogInput;
            }
            else
            {
                return null;
            }
        }

        private async Task CheckAlarm(int value, AnalogInput analogInput)
        {
            Alarm alarm = new Alarm();
            AlarmActivation activation = new AlarmActivation();
            if (analogInput.HighLimit > value)
            {
                if(analogInput.Alarms.Count > 0)
                {
                    alarm = analogInput.Alarms.FirstOrDefault();
                    alarm.TimeStamp = DateTime.Now;
                    _dataContext.Alarms.Update(alarm);
                }
                else
                {
                    Random r = new Random();
                    alarm = new Alarm((int)analogInput.HighLimit, "High value!", analogInput, analogInput.Id, DateTime.Now, r.Next(1, 4), "High", analogInput.Units, false);
                    _dataContext.Alarms.Add(alarm);
                }
            }
            if (analogInput.LowLimit < value)
            {
                if (analogInput.Alarms.Count > 0)
                {
                    alarm = analogInput.Alarms.FirstOrDefault();
                    alarm.TimeStamp = DateTime.Now;
                    _dataContext.Alarms.Update(alarm);
                }
                else
                {
                    Random r = new Random();
                    alarm = new Alarm((int)analogInput.HighLimit, "Low value!", analogInput, analogInput.Id, DateTime.Now, r.Next(1, 4), "Low", analogInput.Units, false);
                    _dataContext.Alarms.Add(alarm);
                }
            }
            activation = new AlarmActivation(DateTime.Now, alarm, alarm.Id);
            _dataContext.AlarmActivations.Add(activation);
            _dataContext.SaveChanges();
            string interpolatedString = $"Alarm id: {alarm.Id}, type: {alarm.Type}, priority: {alarm.Priority}, message: {alarm.Message}, time: {alarm.TimeStamp}, value: {value}.";
            string filePath = "C:\\Users\\ANJA\\Desktop\\SNUS\\snus-backend\\SNUS_PROJECT\\Data\\alarmsLog.txt"; // Replace with the actual file path
            using (StreamWriter writer = new StreamWriter(filePath, append: true))
            {
                writer.WriteLine(interpolatedString);
            }
        }
    }
}
