namespace Snus_project.Models;

public class AnalogOutput : Tag
{
    public double InitialValue { get; set; }
    
    public double LowLimit { get; set; }
    
    public double HighLimit { get; set; }
    
    public string Units { get; set; }


    public AnalogOutput(double initialValue, double lowLimit, double highLimit, string units)
    {
        InitialValue   = initialValue;
        LowLimit       = lowLimit;
        HighLimit      = highLimit;
        Units          = units;
    }
    

    public AnalogOutput(int? id, string name, string ioAddress, string description, double initialValue, double lowLimit, double highLimit, string units) : base(id, name, ioAddress, description)
    {
        InitialValue = initialValue;
        LowLimit = lowLimit;
        HighLimit = highLimit;
        Units = units;
    }
}