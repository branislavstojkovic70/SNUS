namespace Snus_project.Models;

public abstract class Tag
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public string IOAddress { get; set; }
    public string Description { get; set; }


    protected Tag()
    {
    }

    protected Tag(int? id, string name, string ioAddress, string description)
    {
        Id = id;
        Name = name;
        IOAddress = ioAddress;
        Description = description;
    }
}