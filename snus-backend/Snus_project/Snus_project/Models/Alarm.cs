using Snus_project.Models.enums;

namespace Snus_project.Models;

public class Alarm
{
    public int Id { get; set; }
    public double Threshold { get; set; }
    public AlarmType Type { get; set; }
    public int Priority { get; set; }
    public AnalogInput? AnalogInput { get; set; }
    public int AnalogInputId { get; set; }

    public Alarm(int id, double threshold, AlarmType type, int priority, AnalogInput? analogInput, int analogInputId)
    {
        Id            = id;
        Threshold     = threshold;
        Type          = type;
        Priority      = priority;
        AnalogInput   = analogInput;
        AnalogInputId = analogInputId;
    }
}