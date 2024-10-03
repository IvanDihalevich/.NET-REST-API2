using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Courses.Exceptions;
using Domain.Courses;
using MediatR;
using Optional;

namespace Application.Courses.Commands;

public record UpdateCourseCommand : IRequest<Result<Course, CourseException>>
{
    public required Guid CourseId { get; init; }
    public required string Name { get; init; }
    public required DateTime StartAt { get; init; }
    public required DateTime FinishAt { get; init; }
    public required string MaxStudents{ get; init; }
}

public class UpdateCourseCommandHandler(
    ICourseRepository courseRepository) : IRequestHandler<UpdateCourseCommand, Result<Course, CourseException>>
{
    public async Task<Result<Course, CourseException>> Handle(
        UpdateCourseCommand request,
        CancellationToken cancellationToken)
    {
        var courseId = new CourseId(request.CourseId);
        var course = await courseRepository.GetById(courseId, cancellationToken);

        return await course.Match(
            async f =>
            {
                var existingFaculty = await CheckDuplicated(courseId, request.Name, cancellationToken);

                return await existingFaculty.Match(
                    ef => Task.FromResult<Result<Course, CourseException>>(new CourseAlreadyExistsException(ef.Id)),
                    async () => await UpdateEntity(f, request.Name, request.StartAt,request.FinishAt,request.MaxStudents, cancellationToken));
            },
            () => Task.FromResult<Result<Course, CourseException>>(new CourseNotFoundException(courseId)));
    }

    private async Task<Result<Course, CourseException>> UpdateEntity(
        Course course,
        string name,
        DateTime startAt,
        DateTime finishAt,
        string maxStudents,
        CancellationToken cancellationToken)
    {
        try
        {
            course.UpdateDetails(name,startAt, finishAt, maxStudents);

            return await courseRepository.Update(course, cancellationToken);
        }
        catch (Exception exception)
        {
            return new CourseUnknownException(course.Id, exception);
        }
    }

    private async Task<Option<Course>> CheckDuplicated(
        CourseId courseId,
        string name,
        CancellationToken cancellationToken)
    {
        var course = await courseRepository.GetByName(name, cancellationToken);

        return course.Match(
            f => f.Id == courseId ? Option.None<Course>() : Option.Some(f), 
            Option.None<Course>);
    }
}