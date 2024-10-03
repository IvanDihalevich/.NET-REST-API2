using Application.UserCourses.Exceptions;
using Microsoft.AspNetCore.Mvc;

public static class UserCourseErrorHandler
{
    public static ObjectResult ToObjectResult(this UserCourseException exception)
    {
        return new ObjectResult(exception.Message)
        {
            StatusCode = exception switch
            {
                UserCourseNotFoundException => StatusCodes.Status404NotFound,
                UserCourseAlreadyExistsException => StatusCodes.Status409Conflict,
                UserCourseUnknownException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("UserCourse error handler is not implemented")
            }
        };
    }
}