using SNUS_PROJECT.Data;
using SNUS_PROJECT.DTO;
using SNUS_PROJECT.Interfaces;
using SNUS_PROJECT.Models;

namespace SNUS_PROJECT.Repository
{
    public class ReportRepository : IReportRepository
    {
        private DataContext _dataContext;
        private AnalogInputRepository _analogInputRepository;
        private DigitalInputRepository _digitalInputRepository;
        public ReportRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
            _analogInputRepository = new AnalogInputRepository(_dataContext);
            _digitalInputRepository = new DigitalInputRepository(_dataContext);
        }

        public IEnumerable<DigitalInput> GetLatestDigitalInputsPerIOAddress(int sort, DateTime from, DateTime to)
        {
            if (sort == 0)
            {
                return _dataContext.DigitalInputs
               .Where(ai => ai.DateTime >= from && ai.DateTime <= to)
               .OrderBy(a => a.DateTime);
            }
            else
            {
                return _dataContext.DigitalInputs
              .Where(ai => ai.DateTime >= from && ai.DateTime <= to)
               .OrderByDescending(a => a.DateTime);
            }

        }

        public IEnumerable<DigitalOutput> GetLatestDigitalOutputsPerIOAddress(int sort, DateTime from, DateTime to)
        {
            if(sort == 0)
            {
                return _dataContext.DigitalOutputs
                .Where(ai => ai.DateTime >= from && ai.DateTime <= to)
                .OrderBy(a => a.DateTime);
            }
            else
            {
                return _dataContext.DigitalOutputs
                .Where(ai => ai.DateTime >= from && ai.DateTime <= to)
                .OrderByDescending(a => a.DateTime);
            }
        }

        public IEnumerable<AnalogOutput> GetLatestAnalogOutputsPerIOAddress(int sort, DateTime from, DateTime to)
        {
            if (sort == 0)
            {
                return _dataContext.AnalogOutputs
                .Where(ai => ai.DateTime >= from && ai.DateTime <= to)
                .OrderBy(a => a.DateTime);
            }
            else
            {
                return _dataContext.AnalogOutputs
                .Where(ai => ai.DateTime >= from && ai.DateTime <= to)
                .OrderByDescending(a => a.DateTime);
            }
        }

        public IEnumerable<AnalogInput> GetLatestAnalogInputsPerIOAddress(int sort, DateTime from, DateTime to)
        {
            if(sort == 0)
            {
                return _dataContext.AnalogInputs
                .Where(ai => ai.DateTime >= from && ai.DateTime <= to)
                .OrderBy(a => a.DateTime);
            }
            else
            {
                return _dataContext.AnalogInputs
                .Where(ai => ai.DateTime >= from && ai.DateTime <= to)
                .OrderByDescending(a => a.DateTime);
            }
        }

        public ICollection<TagDto> GetLatestValuesOfTags(int sort, DateTime from, DateTime to)
        {
            List<AnalogInput> analogInputs = this.GetLatestAnalogInputsPerIOAddress(sort, from, to).ToList();
            List<AnalogOutput> analogOutputs = this.GetLatestAnalogOutputsPerIOAddress(sort, from, to).ToList();
            List<DigitalInput> digitalInputs = this.GetLatestDigitalInputsPerIOAddress(sort, from, to).ToList();
            List<DigitalOutput> DigitalOutputs = this.GetLatestDigitalOutputsPerIOAddress(sort, from, to).ToList();

            List<TagDto> tags = new List<TagDto>();
            foreach(AnalogInput analogInput in analogInputs)
            {
                tags.Add(new TagDto(analogInput));
            }

            foreach(AnalogOutput analogOutput in analogOutputs)
            {
                tags.Add(new TagDto(analogOutput));
            }
            foreach(DigitalInput digitalInput in digitalInputs)
            {
                tags.Add(new TagDto(digitalInput));
            }
            foreach (DigitalOutput digitalOutput in DigitalOutputs)
            {
                tags.Add(new TagDto(digitalOutput));
            }

            if(sort == 0)
            {
                return tags.OrderBy(t => t.DateTime).ToList();
            }
            else
            {
                return tags.OrderByDescending(t => t.DateTime).ToList();
            }
        }

        public ICollection<TagDto> GetLatestValuesOfAITags(int sort)
        {
            List<AnalogInput> analogInputs = _analogInputRepository.GetLatestAnalogInputsPerIOAddress().ToList();
            List<TagDto> tags = new List<TagDto>();
            if (analogInputs.Count!=0)
            {
                foreach (AnalogInput analogInput in analogInputs)
                {
                    tags.Add(new TagDto(analogInput));
                }
            }
            Console.WriteLine(tags.Count);
            if (sort == 0)
            {
                return tags.OrderBy(t => t.DateTime).ToList();
            }
            else
            {
                return tags.OrderByDescending(t => t.DateTime).ToList();
            }
        }

        public ICollection<TagDto> GetLatestValuesOfDITags(int sort)
        {
            List<DigitalInput> DigitalInputs = _digitalInputRepository.GetLatestDigitalInputsPerIOAddress().ToList();
            List<TagDto> tags = new List<TagDto>();
            foreach (DigitalInput DigitalInput in DigitalInputs)
            {
                tags.Add(new TagDto(DigitalInput));
            }
            if (sort == 0)
            {
                return tags.OrderBy(t => t.DateTime).ToList();
            }
            else
            {
                return tags.OrderByDescending(t => t.DateTime).ToList();
            }
        }

    }
}
