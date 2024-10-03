namespace Domain.Courses;

public record CourseId(Guid Value)
{
    public static CourseId Empty => new(Guid.Empty);
    public static CourseId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}