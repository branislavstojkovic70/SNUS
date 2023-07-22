namespace Snus_project.Models;

public class AnalogInput : Tag
{
    public double ScanTime { get; set; }
    
    public List<Alarm> Alarms { get; set; } = new List<Alarm>();
    
    public bool IsOn { get; set; }
    
    public double LowLimit { get; set; }
   
    public double HighLimit { get; set; }
    
    public string Units { get; set; }

    public AnalogInput(int? id, string name, string ioAddress, string description, double scanTime, bool isOn, double lowLimit, double highLimit, string units) : base(id, name, ioAddress, description)
    {
        ScanTime  = scanTime;
        IsOn      = isOn;
        LowLimit  = lowLimit;
        HighLimit = highLimit;
        Units     = units;
    }

    public AnalogInput(int? id, string name, string ioAddress, string description) : base(id, name, ioAddress, description){}
    
    public AnalogInput(double scanTime, bool isOn, double lowLimit, double highLimit, string units)
    {
        ScanTime       = scanTime;
        IsOn           = isOn;
        LowLimit       = lowLimit;
        HighLimit      = highLimit;
        Units          = units;
    }
}