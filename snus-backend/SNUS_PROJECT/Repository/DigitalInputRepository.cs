using SNUS_PROJECT.Data;
using SNUS_PROJECT.DTO;
using SNUS_PROJECT.Interfaces;
using SNUS_PROJECT.Models;

namespace SNUS_PROJECT.Repository
{
    public class DigitalInputRepository : IDigitalInputRepository
    {
        private DataContext _dataContext;
        public DigitalInputRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public void AddDigitalInput(DigitalInput DigitalInput)
        {
            _dataContext.DigitalInputs.Add(DigitalInput);
            _dataContext.SaveChanges();
        }

        public void DeleteDigitalInput(int id)
        {
            var aiToRemove = _dataContext.DigitalInputs.Where(p => p.Id == id).FirstOrDefault();

            if (aiToRemove != null)
            {
                _dataContext.DigitalInputs.Remove(aiToRemove);
                _dataContext.SaveChanges();
            }
        }

        public DigitalInput GetDigitalInput(int id)
        {
            return _dataContext.DigitalInputs.First(u => u.Id == id);
        }

        public ICollection<DigitalInput> GetDigitalInputs()
        {
            return _dataContext.DigitalInputs.OrderBy(p => p.Id).ToList();
        }

        public bool? IsDigitalInputActive(int id)
        {
            var DigitalInput = _dataContext.DigitalInputs.Where(p => p.Id == id).FirstOrDefault();
            if (!DigitalInput.Equals(null))
            {
                return DigitalInput.IsActive;
            }
            else
            {
                return false;
            }
        }

        public void UpdateDigitalInput(DigitalInputDto DigitalInputDto, int id)
        {
            var existingDigitalInput = _dataContext.DigitalInputs.Where(p => p.Id == id).FirstOrDefault();
            if (existingDigitalInput != null)
            {
                existingDigitalInput.Name = DigitalInputDto.Name;
                existingDigitalInput.Description = DigitalInputDto.Description;
                existingDigitalInput.IOAddress = DigitalInputDto.IOAddress;
                existingDigitalInput.Driver = DigitalInputDto.Driver;
                existingDigitalInput.ScanTime = DigitalInputDto.ScanTime;
                _dataContext.SaveChanges();
            }
        }
    }
}
