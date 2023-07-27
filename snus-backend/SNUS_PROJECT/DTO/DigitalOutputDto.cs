namespace SNUS_PROJECT.DTO
{
    public class DigitalOutputDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IOAddress { get; set; }
        public double InitialValue { get; set; }
        public DateTime DateTime { get; set; }
        public double Value { get; set; }

        public DigitalOutputDto() { }

        public DigitalOutputDto(string name, string description, string iOAddress, double initialValue, DateTime dateTime, double value)
        {
            Name = name;
            Description = description;
            IOAddress = iOAddress;
            InitialValue = initialValue;
            DateTime = dateTime;
            Value = value;
        }
    }
}
