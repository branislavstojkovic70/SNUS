﻿using SNUS_PROJECT.DTO;
using SNUS_PROJECT.Models;

namespace SNUS_PROJECT.Interfaces
{
    public interface IDigitalOutputRepository
    {
        ICollection<DigitalOutput> GetDigitalOutputs();
        DigitalOutput GetDigitalOutput(int id);
        DigitalOutput GetDigitalOutputByName(string name);
        void AddDigitalOutput(DigitalOutput digitalOutput);
        void UpdateDigitalOutput(DigitalOutputDto digitalOutputDto, int id);
        void DeleteDigitalOutput(int id);
        IEnumerable<DigitalOutput> GetLatestDigitalOutputsPerIOAddress();
        public ICollection<TagDto> GetDigitalOutputsById(string name, int sort);

    }
}
