using Domain.Faculties;

namespace Application.Faculties.Exceptions;

public abstract class FacultyException(FacultyId id, string message, Exception? innerException = null)
    : Exception(message, innerException)
{
    public FacultyId FacultyId { get; } = id;
}

public class FacultyNotFoundException(FacultyId id) : FacultyException(id, $"Faculty under id: {id} not found");

public class FacultyAlreadyExistsException(FacultyId id) : FacultyException(id, $"Faculty already exists: {id}");

public class FacultyUnknownException(FacultyId id, Exception innerException)
    : FacultyException(id, $"Unknown exception for the faculty under id: {id}", innerException);