namespace Snus_project.Models;

public class DigitalInput
{
    public double ScanTime { get; set; }
    
    public bool IsOn { get; set; }

    public DigitalInput(double scanTime, bool isOn)
    {
        ScanTime = scanTime;
        IsOn     = isOn;
    }
}