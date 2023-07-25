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

        public ICollection<AnalogOutput> GetAnalogOutputs()
        {
            return _dataContext.AnalogOutputs.OrderBy(p => p.Id).ToList();
        }


        public void UpdateAnalogOutput(AnalogOutputDto analogOutputDto, int id)
        {
            var existingAnalogOutput = _dataContext.AnalogOutputs.Where(p => p.Id == id).FirstOrDefault();
            if (existingAnalogOutput != null)
            {
                existingAnalogOutput.Name = analogOutputDto.Name;
                existingAnalogOutput.Description = analogOutputDto.Description;
                existingAnalogOutput.IOAddress = analogOutputDto.IOAddress;
                existingAnalogOutput.InitialValue = analogOutputDto.InitialValue;
                existingAnalogOutput.LowLimit = analogOutputDto.LowLimit;
                existingAnalogOutput.HighLimit = analogOutputDto.HighLimit;
                existingAnalogOutput.Units = analogOutputDto.Units;
                _dataContext.SaveChanges();
            }
        }
    }
}
