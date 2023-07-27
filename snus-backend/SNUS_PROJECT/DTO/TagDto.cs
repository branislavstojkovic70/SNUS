using SNUS_PROJECT.Models;

namespace SNUS_PROJECT.DTO
{
    public class TagDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double Value { get; set; }

        public DateTime DateTime { get; set; }

        public TagDto() { }

        public TagDto(string name, string description, double value, DateTime dateTime)
        {
            Name = name;    
            Description = description;
            Value = value;
            DateTime = dateTime;
        }

        public TagDto(AnalogInput analogInput)
        {
            Name = analogInput.Name;
            Description = analogInput.Description;
            DateTime = analogInput.DateTime;
            Value = analogInput.Value;
        }
        public TagDto(DigitalInput digitalInput)
        {
            Name = digitalInput.Name;
            Description = digitalInput.Description;
            DateTime = digitalInput.DateTime;
            Value = digitalInput.Value;
        }

        public TagDto(DigitalOutput digitalOutput)
        {
            Name = digitalOutput.Name;
            Description = digitalOutput.Description;
            DateTime = digitalOutput.DateTime;
            Value = digitalOutput.Value;
        }
        public TagDto(AnalogOutput analogOutput)
        {
            Name = analogOutput.Name;
            Description = analogOutput.Description;
            DateTime = analogOutput.DateTime;
            Value = analogOutput.Value;
        }
    }
}
