using SNUS_PROJECT.DTO;
using SNUS_PROJECT.Models;

namespace SNUS_PROJECT.Interfaces
{
    public interface IDigitalInputRepository
    {
        ICollection<DigitalInput> GetDigitalInputs();
        DigitalInput GetDigitalInput(int id);
        bool? IsDigitalInputActive(int id);
        void AddDigitalInput(DigitalInput digitalInput);
        void UpdateDigitalInput(DigitalInputDto digitalInputDto, int id);
        void DeleteDigitalInput(int id);
        void TurnOnDI(int id);
        void TurnOffDI(int id);
        IEnumerable<DigitalInput> GetLatestDigitalInputsPerIOAddress();
        public ICollection<TagDto> GetDigitalInputsById(string name, int sort);

    }
}
