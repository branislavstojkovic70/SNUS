using SNUS_PROJECT.Data;
using SNUS_PROJECT.DTO;
using SNUS_PROJECT.Interfaces;
using SNUS_PROJECT.Models;

namespace SNUS_PROJECT.Repository
{
    public class AnalogOutputRepository : IAnalogOutputRepository
    {
        private DataContext _dataContext;
        public AnalogOutputRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public void AddAnalogOutput(AnalogOutput analogOutput)
        {
            _dataContext.AnalogOutputs.Add(analogOutput);
            _dataContext.SaveChanges();
        }

        public void DeleteAnalogOutput(int id)
        {
            var aiToRemove = _dataContext.AnalogOutputs.Where(p => p.Id == id).FirstOrDefault();

            if (aiToRemove != null)
            {
                _dataContext.AnalogOutputs.Remove(aiToRemove);
                _dataContext.SaveChanges();
            }
        }

        public AnalogOutput GetAnalogOutput(int id)
        {
            return _dataContext.AnalogOutputs.First(u => u.Id == id);
        }
        public AnalogOutput GetAnalogOutputByName(string name)
        {
            return _dataContext.AnalogOutputs.First(u => u.Name == name);
        }

        public ICollection<AnalogOutput> GetAnalogOutputs()
        {
            return _dataContext.AnalogOutputs.OrderBy(p => p.Id).ToList();
        }


        public void UpdateAnalogOutput(AnalogOutputDto analogOutputDto, int id)
        {
            var existingAnalogOutput = _dataContext.AnalogOutputs.Where(p => p.Id == id).FirstOrDefault();
            if (existingAnalogOutput != null)
            {
                _dataContext.Attach(existingAnalogOutput);
                _dataContext.Entry(existingAnalogOutput).CurrentValues.SetValues(analogOutputDto);
                _dataContext.SaveChanges();
            }
        }

        public IEnumerable<AnalogOutput> GetLatestAnalogOutputsPerIOAddress()
        {
            var latestInputs = _dataContext.AnalogOutputs
                .GroupBy(a => a.IOAddress)
                .Select(g => g.OrderByDescending(a => a.DateTime).FirstOrDefault());

            return latestInputs;
        }

        public ICollection<TagDto> GetAnalogOutputsById(string name, int sort)
        {
            List<TagDto> result = new List<TagDto>();
            if (sort == 0)
            {
                List<AnalogOutput> ais = _dataContext.AnalogOutputs.Where(p => p.Name==name).OrderBy(ai => ai.Value).ToList();
                foreach (var ai in ais)
                {
                    result.Add(new TagDto(ai));
                }
            }
            else
            {
                List<AnalogOutput> ais = _dataContext.AnalogOutputs.Where(p => p.Name == name).OrderByDescending(ai => ai.Value).ToList();
                foreach (var ai in ais)
                {
                    result.Add(new TagDto(ai));
                }
            }
            return result;
            
        }
    }
}
