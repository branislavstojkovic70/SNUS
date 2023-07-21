namespace Snus_project.Models;

public class DigitalOutput : Tag
{
    public int InitialValue { get; set; }


    public DigitalOutput(int initialValue)
    {
        InitialValue = initialValue;
    }
}