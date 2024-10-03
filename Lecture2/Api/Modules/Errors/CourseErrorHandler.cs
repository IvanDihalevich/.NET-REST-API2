using Application.Courses.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Errors;

public static class CourseErrorHandler
{
    public static ObjectResult ToObjectResult(this CourseException exception)
    {
        return new ObjectResult(exception.Message)
        {
            StatusCode = exception switch
            {
                CourseNotFoundException => StatusCodes.Status404NotFound,
                CourseAlreadyExistsException => StatusCodes.Status409Conflict,
                CourseUnknownException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("Course error handler does not implemented")
            }
        };
    }
}