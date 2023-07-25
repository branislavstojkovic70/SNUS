namespace SNUS_PROJECT.DTO
{
    public class AnalogOutputDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IOAddress { get; set; }
        public double InitialValue { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public string Units { get; set; }

        public AnalogOutputDto() { }

        public AnalogOutputDto(string name, string description, string iOAddress, double initialValue, double lowLimit, double highLimit, string units)
        {
            Name = name;
            Description = description;
            IOAddress = iOAddress;
            InitialValue = initialValue;
            LowLimit = lowLimit;
            HighLimit = highLimit;
            Units = units;
        }
    }
}
