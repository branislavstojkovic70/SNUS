using SNUS_PROJECT.DTO;
using SNUS_PROJECT.Models;

namespace SNUS_PROJECT.Interfaces
{
    public interface IReportRepository
    {
        IEnumerable<DigitalInput> GetLatestDigitalInputsPerIOAddress(int sort, DateTime from, DateTime to);
        IEnumerable<DigitalOutput> GetLatestDigitalOutputsPerIOAddress(int sort, DateTime from, DateTime to);
        IEnumerable<AnalogOutput> GetLatestAnalogOutputsPerIOAddress(int sort, DateTime from, DateTime to);
        IEnumerable<AnalogInput> GetLatestAnalogInputsPerIOAddress(int sort, DateTime from, DateTime to);
        List<TagDto> GetLatestValuesOfTags(int sort, DateTime from, DateTime to);
        IEnumerable<TagDto> GetLatestValuesOfAITags(int sort);
        IEnumerable<TagDto> GetLatestValuesOfDITags(int sort);
    }
}
