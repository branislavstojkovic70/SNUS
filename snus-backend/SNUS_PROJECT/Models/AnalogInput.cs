using SNUS_PROJECT.DTO;

namespace SNUS_PROJECT.Models
{
    public class AnalogInput
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? IOAddress { get; set; }
        public string? Driver { get; set; }
        public double ScanTime { get; set; }
        public bool? IsActive { get; set; }
        public ICollection<Alarm> Alarms { get; set; } = new List<Alarm>();
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public string? Units { get; set; }

        public AnalogInput() { }

        public AnalogInput(string? name, string? description, string? iOAddress, string? driver, double scanTime, bool isActive, ICollection<Alarm> alarms, double lowLimit, double highLimit, string? units)
        {
            Name = name;
            Description = description;
            IOAddress = iOAddress;
            Driver = driver;
            ScanTime = scanTime;
            IsActive = isActive;
            Alarms = alarms;
            LowLimit = lowLimit;
            HighLimit = highLimit;
            Units = units;
        }

        public AnalogInput(AnalogInputDto analogInputDto)
        {
            Name = analogInputDto.Name;
            Description = analogInputDto.Description;
            IOAddress = analogInputDto.IOAddress;
            Driver = analogInputDto.Driver;
            ScanTime = analogInputDto.ScanTime;
            IsActive = true;
            Alarms = new List<Alarm>();
            LowLimit = analogInputDto.LowLimit;
            HighLimit = analogInputDto.HighLimit;
            Units = analogInputDto.Units;
        }
    }
}
