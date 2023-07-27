using SNUS_PROJECT.Data;
using SNUS_PROJECT.DTO;
using SNUS_PROJECT.Interfaces;
using SNUS_PROJECT.Models;

namespace SNUS_PROJECT.Repository
{
    public class DigitalOutputRepository : IDigitalOutputRepository
    {
        private DataContext _dataContext;
        public DigitalOutputRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public void AddDigitalOutput(DigitalOutput digitalOutput)
        {
            _dataContext.DigitalOutputs.Add(digitalOutput);
            _dataContext.SaveChanges();
        }

        public void DeleteDigitalOutput(int id)
        {
            var aiToRemove = _dataContext.DigitalOutputs.Where(p => p.Id == id).FirstOrDefault();

            if (aiToRemove != null)
            {
                _dataContext.DigitalOutputs.Remove(aiToRemove);
                _dataContext.SaveChanges();
            }
        }

        public DigitalOutput GetDigitalOutput(int id)
        {
            return _dataContext.DigitalOutputs.First(u => u.Id == id);
        }

        public ICollection<DigitalOutput> GetDigitalOutputs()
        {
            return _dataContext.DigitalOutputs.OrderBy(p => p.Id).ToList();
        }


        public void UpdateDigitalOutput(DigitalOutputDto digitalOutputDto, int id)
        {
            var existingDigitalOutput = _dataContext.DigitalOutputs.Where(p => p.Id == id).FirstOrDefault();
            if (existingDigitalOutput != null)
            {
                existingDigitalOutput.Name = digitalOutputDto.Name;
                existingDigitalOutput.Description = digitalOutputDto.Description;
                existingDigitalOutput.IOAddress = digitalOutputDto.IOAddress;
                existingDigitalOutput.InitialValue = digitalOutputDto.InitialValue;
                existingDigitalOutput.DateTime = digitalOutputDto.DateTime;
                _dataContext.SaveChanges();
            }
        }

        public IEnumerable<DigitalOutput> GetLatestDigitalOutputsPerIOAddress()
        {
            var latestInputs = _dataContext.DigitalOutputs
                .GroupBy(a => a.IOAddress)
                .Select(g => g.OrderByDescending(a => a.DateTime).FirstOrDefault());

            return latestInputs;
        }

        public ICollection<TagDto> GetDigitalOutputsById(string name, int sort)
        {
            List<TagDto> result = new List<TagDto>();
            if (sort == 0)
            {
                List<DigitalOutput> ais = _dataContext.DigitalOutputs.Where(p => (p.Name ?? "").Equals(name, StringComparison.OrdinalIgnoreCase)).OrderBy(ai => ai.Value).ToList();
                foreach (var ai in ais)
                {
                    result.Add(new TagDto(ai));
                }
            }
            else
            {
                List<DigitalOutput> ais = _dataContext.DigitalOutputs.Where(p => (p.Name ?? "").Equals(name, StringComparison.OrdinalIgnoreCase)).OrderByDescending(ai => ai.Value).ToList();
                foreach (var ai in ais)
                {
                    result.Add(new TagDto(ai));
                }
            }
            return result;
           
        }
    }
}

