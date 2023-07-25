using SNUS_PROJECT.DTO;
using System.Net;

namespace SNUS_PROJECT.Models
{
    public class AnalogOutput
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? IOAddress { get; set; }
        public double InitialValue { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public string? Units { get; set; }

        public AnalogOutput() { }

        public AnalogOutput(string? name, string? description, string? iOAddress, double initialValue, double lowLimit, double highLimit, string? units)
        {
            Name = name;
            Description = description;
            IOAddress = iOAddress;
            InitialValue = initialValue;
            LowLimit = lowLimit;
            HighLimit = highLimit;
            Units = units;
        }

      
        public AnalogOutput(AnalogOutputDto analogOutputDto)
        {
            Name = analogOutputDto.Name;
            Description = analogOutputDto.Description;
            IOAddress = analogOutputDto.IOAddress;
            InitialValue = analogOutputDto.InitialValue;
            LowLimit = analogOutputDto.LowLimit;
            HighLimit = analogOutputDto.HighLimit;
            Units = analogOutputDto.Units;
        }
    }
}
