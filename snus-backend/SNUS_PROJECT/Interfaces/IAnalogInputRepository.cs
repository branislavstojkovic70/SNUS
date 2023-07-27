﻿using SNUS_PROJECT.DTO;
using SNUS_PROJECT.Models;

namespace SNUS_PROJECT.Interfaces
{
    public interface IAnalogInputRepository
    {
        ICollection<AnalogInput> GetAnalogInputs();
        AnalogInput GetAnalogInput(int id);
        bool? IsAnalogInputActive(int id);
        void AddAnalogInput(AnalogInput analogInput);
        void UpdateAnalogInput(AnalogInputDto analogInputDto, int id);
        void DeleteAnalogInput(int id);
        IEnumerable<AnalogInput> GetLatestAnalogInputsPerIOAddress();
        void TurnOnAI(int id);
        void TurnOffAI(int id);

        public ICollection<TagDto> GetAnalogInputsById(string name, int sort);
    }
}
