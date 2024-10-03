using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Courses.Exceptions;
using Domain.Courses;
using MediatR;

namespace Application.Courses.Commands;

public record CreateCourseCommand : IRequest<Result<Course, CourseException>>
{
    public required string Name { get; init; }
    public required DateTime StartAt { get; init; }
    public required DateTime FinishAt { get; init; }
    public required string MaxStudents{ get; init; }
}

public class CreateCourseCommandHandler(
    ICourseRepository courseRepository) : IRequestHandler<CreateCourseCommand, Result<Course, CourseException>>
{
    public async Task<Result<Course, CourseException>> Handle(
        CreateCourseCommand request,
        CancellationToken cancellationToken)
    {
        var existingFaculty = await courseRepository.GetByName(request.Name, cancellationToken);

        return await existingFaculty.Match(
            f => Task.FromResult<Result<Course, CourseException>>(new CourseAlreadyExistsException(f.Id)),
            async () => await CreateEntity(request.Name, request.StartAt,request.FinishAt, request.MaxStudents, cancellationToken));
    }

    private async Task<Result<Course, CourseException>> CreateEntity(
        string name,
        DateTime startAt,
        DateTime finishAt,
        string maxStudents,
        CancellationToken cancellationToken)
    {
        try
        {
            var entity = Course.New(CourseId.New(), name, startAt, finishAt, maxStudents);

            return await courseRepository.Add(entity, cancellationToken);
        }
        catch (Exception exception)
        {
            return new CourseUnknownException(CourseId.Empty, exception);
        }
    }
}