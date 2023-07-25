using SNUS_PROJECT.DTO;
using System;

namespace SNUS_PROJECT.Models
{
    public class DigitalInput
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? IOAddress { get; set; }
        public string? Driver { get; set; }
        public double ScanTime { get; set; }

        public bool? IsActive { get; set; }

        public DigitalInput(string? name, string? description, string? iOAddress, string? driver, double scanTime, bool? isActive)
        {
            Name = name;
            Description = description;
            IOAddress = iOAddress;
            Driver = driver;
            ScanTime = scanTime;
            IsActive = isActive;
        }

        public DigitalInput()
        {
        }

        public DigitalInput(DigitalInputDto digitalInputDto)
        {
            Name = digitalInputDto.Name;
            Description = digitalInputDto.Description;
            IOAddress = digitalInputDto.IOAddress;
            Driver = digitalInputDto.Driver;
            ScanTime = digitalInputDto.ScanTime;
            IsActive = digitalInputDto.IsActive;
        }
    }
}
