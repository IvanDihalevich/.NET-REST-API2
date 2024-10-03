using Domain.Faculties;

namespace Domain.Courses;

public class Course
{
    public CourseId Id { get; }
    public string Name { get; private set; }
    public DateTime StartAt   { get; private set; }
    public DateTime FinishAt    { get; private set; }
    public string MaxStudents  { get; private set; }
    public DateTime UpdatedAt { get; private set; }


    private Course(CourseId id, string name, DateTime startAt, DateTime finishAt, string maxStudents, DateTime updatedAt)
    {
        Id = id;
        Name = name;
        StartAt = startAt;  
        FinishAt = finishAt;
        MaxStudents = maxStudents; 
        UpdatedAt = updatedAt;
    }

    public static Course New(CourseId id, string name, DateTime startAt, DateTime finishAt, string maxStudents)
        => new(id, name, startAt, finishAt, maxStudents, DateTime.UtcNow);
    
    public void UpdateDetails(string name, DateTime startAt, DateTime finishAt, string maxStudents)
    {
        Name = name;
        StartAt = startAt;
        FinishAt = finishAt;
        MaxStudents = maxStudents;
        UpdatedAt = DateTime.UtcNow;
    }
}