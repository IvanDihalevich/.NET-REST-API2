namespace Domain.UserCourse;

public record UserCourseId(Guid Value)
{
    public static UserCourseId Empty => new(Guid.Empty);
    public static UserCourseId New() => new (Guid.NewGuid());
    public override string ToString() => Value.ToString();
}