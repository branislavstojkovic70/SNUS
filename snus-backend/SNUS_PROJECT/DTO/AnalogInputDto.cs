using SNUS_PROJECT.Models;

namespace SNUS_PROJECT.DTO
{
    public class AnalogInputDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IOAddress { get; set; }
        public string Driver { get; set; }
        public double ScanTime { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public string Units { get; set; }

        public AnalogInputDto(string name, string description, string iOAddress, string driver, double scanTime, double lowLimit, double highLimit, string units)
        {
            Name = name;
            Description = description;
            IOAddress = iOAddress;
            Driver = driver;
            ScanTime = scanTime;
            LowLimit = lowLimit;
            HighLimit = highLimit;
            Units = units;
        }

        public AnalogInputDto()
        {
        }
    }
}
