using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Courses.Exceptions;
using Domain.Courses;
using MediatR;

namespace Application.Courses.Commands;

public record DeleteCourseCommand : IRequest<Result<Course, CourseException>>
{
    public required Guid CourseId { get; init; }
}

public class DeleteCourseCommandHandler(ICourseRepository courseRepository)
    : IRequestHandler<DeleteCourseCommand, Result<Course, CourseException>>
{
    public async Task<Result<Course, CourseException>> Handle(
        DeleteCourseCommand request,
        CancellationToken cancellationToken)
    {
        var courseId = new CourseId(request.CourseId);

        var existingCourse = await courseRepository.GetById(courseId, cancellationToken);

        return await existingCourse.Match<Task<Result<Course, CourseException>>>(
            async u => await DeleteEntity(u, cancellationToken),
            () => Task.FromResult<Result<Course, CourseException>>(new CourseNotFoundException(courseId)));
    }

    public async Task<Result<Course, CourseException>> DeleteEntity(Course course, CancellationToken cancellationToken)
    {
        try
        {
            return await courseRepository.Delete(course, cancellationToken);
        }
        catch (Exception exception)
        {
            return new CourseUnknownException(course.Id, exception);
        }
    }
}