namespace Domain.Faculties;

public class Faculty
{
    public FacultyId Id { get; }

    public string Name { get; private set; }

    private Faculty(FacultyId id, string name)
    {
        Id = id;
        Name = name;
    }

    public static Faculty New(FacultyId id, string name)
        => new(id, name);

    public void UpdateDetails(string name)
    {
        Name = name;
    }
}