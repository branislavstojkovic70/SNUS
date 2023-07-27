using SNUS_PROJECT.DTO;

namespace SNUS_PROJECT.Models
{
    public class DigitalOutput
    {
        /*- tag name (id)
            - description
            - I/O address
            - initial value*/
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? IOAddress { get; set; }
        public double InitialValue { get; set; }
        public DateTime DateTime { get; set; }

        public double Value { get; set; }

        public DigitalOutput() { }

        public DigitalOutput(string? name, string? description, string? iOAddress, double initialValue, DateTime dateTime, double value)
        {
            Name = name;
            Description = description;
            IOAddress = iOAddress;
            InitialValue = initialValue;
            DateTime = dateTime;
            Value = value;
        }

        public DigitalOutput(DigitalOutputDto digitalOutputDto)
        {
            Name = digitalOutputDto.Name;
            Description = digitalOutputDto.Description;
            IOAddress = digitalOutputDto.IOAddress;
            InitialValue = digitalOutputDto.InitialValue;
            DateTime = digitalOutputDto.DateTime;
            Value = digitalOutputDto.Value; 
        }
    }
}
