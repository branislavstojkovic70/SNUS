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
                _dataContext.SaveChanges();
            }
        }
    }
}

