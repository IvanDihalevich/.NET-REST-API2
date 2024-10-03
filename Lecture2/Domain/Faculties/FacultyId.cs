namespace Domain.Faculties;

public record FacultyId(Guid Value)
{
    public static FacultyId Empty => new(Guid.Empty);
    public static FacultyId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}