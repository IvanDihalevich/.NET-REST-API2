using Application.Faculties.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Errors;

public static class FacultyErrorHandler
{
    public static ObjectResult ToObjectResult(this FacultyException exception)
    {
        return new ObjectResult(exception.Message)
        {
            StatusCode = exception switch
            {
                FacultyNotFoundException => StatusCodes.Status404NotFound,
                FacultyAlreadyExistsException => StatusCodes.Status409Conflict,
                FacultyUnknownException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("Faculty error handler does not implemented")
            }
        };
    }
}