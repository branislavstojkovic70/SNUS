namespace SNUS_PROJECT.DTO
{
    public class DigitalInputDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IOAddress { get; set; }
        public string Driver { get; set; }
        public double ScanTime { get; set; }

        public bool? IsActive { get; set; }
        public DateTime DateTime { get; set; }
        public double Value { get; set; }

        public DigitalInputDto() { }

        public DigitalInputDto(string name, string description, string iOAddress, string driver, double scanTime, bool? isActive, DateTime dateTime, double value)
        {
            Name = name;
            Description = description;
            IOAddress = iOAddress;
            Driver = driver;
            ScanTime = scanTime;
            IsActive = isActive;
            DateTime = dateTime;
            Value = value;
        }
    }
}
