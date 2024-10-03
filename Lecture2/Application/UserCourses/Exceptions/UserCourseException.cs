using Domain.UserCourse;

namespace Application.UserCourses.Exceptions;

public abstract class UserCourseException : Exception
{
    protected UserCourseException(UserCourseId id, string message, Exception? innerException = null)
        : base(message, innerException)
    {
        UserCourseId = id;
    }

    public UserCourseId UserCourseId { get; }
}

public class UserCourseNotFoundException : UserCourseException
{
    public UserCourseNotFoundException(UserCourseId id) 
        : base(id, $"User Course under id: {id} not found") { }
}

public class UserCourseAlreadyExistsException : UserCourseException
{
    public UserCourseAlreadyExistsException(UserCourseId id) 
        : base(id, $"User Course already exists: {id}") { }
}

public class UserCourseUnknownException : UserCourseException
{
    public UserCourseUnknownException(UserCourseId id, Exception innerException) 
        : base(id, $"Unknown exception for the User Course under id: {id}", innerException) { }
}