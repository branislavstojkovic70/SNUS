using SNUS_PROJECT.DTO;
using SNUS_PROJECT.Models;

namespace SNUS_PROJECT.Interfaces
{
    public interface IAnalogInputRepository
    {
        ICollection<AnalogInput> GetAnalogInputs();
        AnalogInput GetAnalogInput(int id);
        AnalogInput GetAnalogInputByName(string name);
        bool? IsAnalogInputActive(int id);
        void AddAnalogInput(AnalogInput analogInput);
        void UpdateAnalogInput(AnalogInputDto analogInputDto, int id);
        AnalogInput? ChangeValue(int id, int value);
        void DeleteAnalogInput(int id);
        IEnumerable<AnalogInput> GetLatestAnalogInputsPerIOAddress();
        bool TurnOnOffAI(int id);

        public ICollection<TagDto> GetAnalogInputsById(string name, int sort);
        public List<AnalogInput> GetAnalogInputsByName(string name, int sort);
    }
}
