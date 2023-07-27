using SNUS_PROJECT.DTO;
using SNUS_PROJECT.Models;

namespace SNUS_PROJECT.Interfaces
{
    public interface IAnalogOutputRepository
    {
        ICollection<AnalogOutput> GetAnalogOutputs();
        AnalogOutput GetAnalogOutput(int id);
        void AddAnalogOutput(AnalogOutput analogOutput);
        void UpdateAnalogOutput(AnalogOutputDto analogOutputDto, int id);
        void DeleteAnalogOutput(int id);
        IEnumerable<AnalogOutput> GetLatestAnalogOutputsPerIOAddress();
        public ICollection<TagDto> GetAnalogOutputsById(string name, int sort);

    }
}
