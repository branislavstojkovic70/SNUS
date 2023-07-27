using SNUS_PROJECT.Data;
using SNUS_PROJECT.DTO;
using SNUS_PROJECT.Interfaces;
using SNUS_PROJECT.Models;

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

        public ICollection<AnalogInput> GetAnalogInputs()
        {
            return _dataContext.AnalogInputs.OrderBy(p => p.Id).ToList();
        }
        public ICollection<TagDto> GetAnalogInputsById(string name, int sort)
        {
            List<TagDto> result = new List<TagDto>();
            if (sort == 0)
            {
                List<AnalogInput> ais = _dataContext.AnalogInputs.Where(p => (p.Name ?? "").Equals(name, StringComparison.OrdinalIgnoreCase)).OrderBy(ai => ai.Value).ToList();
                foreach (var ai in ais)
                {
                    result.Add(new TagDto(ai));
                }
            }
            else
            {
                List<AnalogInput> ais = _dataContext.AnalogInputs.Where(p => (p.Name ?? "").Equals(name, StringComparison.OrdinalIgnoreCase)).OrderByDescending(ai => ai.Value).ToList();
                foreach (var ai in ais)
                {
                    result.Add(new TagDto(ai));
                }
            }
            return result;
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
                
                _dataContext.SaveChanges();
            }
        }

        public IEnumerable<AnalogInput> GetLatestAnalogInputsPerIOAddress()
        {
            var latestInputs = _dataContext.AnalogInputs
                .GroupBy(a => a.IOAddress)
                .Select(g => g.OrderByDescending(a => a.DateTime).FirstOrDefault());

            return latestInputs;
        }

        public void TurnOnAI(int id)
        {
            var existingAnalogInput = _dataContext.AnalogInputs.Where(p => p.Id == id).FirstOrDefault();
            if (existingAnalogInput != null)
            {              
                existingAnalogInput.IsActive = true;
                _dataContext.SaveChanges();
            }
        }

        public void TurnOffAI(int id)
        {
            var existingAnalogInput = _dataContext.AnalogInputs.Where(p => p.Id == id).FirstOrDefault();
            if (existingAnalogInput != null)
            {
                existingAnalogInput.IsActive = false;
                _dataContext.SaveChanges();
            }
        }
    }
}
