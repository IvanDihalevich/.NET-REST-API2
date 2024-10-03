using Domain.Courses;
using Domain.Faculties;

namespace Domain.Users;

public class User
{
    public UserId Id { get; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string FullName { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public FacultyId FacultyId { get; }
    public Faculty? Faculty { get; }

    public List<Course> Courses { get; set; } = new();
    private User(UserId id, string firstName, string lastName, DateTime updatedAt, FacultyId facultyId)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        UpdatedAt = updatedAt;
        FacultyId = facultyId;
        FullName = $"{firstName} {lastName}";
    }

    public static User New(UserId id, string firstName, string lastName, FacultyId facultyId)
        => new(id, firstName, lastName, DateTime.UtcNow, facultyId);

    public void UpdateDetails(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        FullName = $"{firstName} {lastName}";
        UpdatedAt = DateTime.UtcNow;
    }
}