using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.UserCourses.Exceptions;
using Domain.UserCourse;
using Domain.Users;
using Domain.Courses;
using MediatR;

namespace Application.UserCourses.Commands;

public record CreateUserCourseCommand : IRequest<Result<UserCourse, UserCourseException>>
{
    public required Guid CourseId { get; init; }
    public required Guid UserId { get; init; }
    public required int Rating { get; init; }
    public required DateTime JoinAt { get; init; }
    public required DateTime EndAt { get; init; }
}

public class CreateUserCourseCommandHandler : IRequestHandler<CreateUserCourseCommand, Result<UserCourse, UserCourseException>>
{
    private readonly IUserCourseRepository _userCourseRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICourseRepository _courseRepository;

    public CreateUserCourseCommandHandler(
        IUserCourseRepository userCourseRepository,
        IUserRepository userRepository,
        ICourseRepository courseRepository)
    {
        _userCourseRepository = userCourseRepository;
        _userRepository = userRepository;
        _courseRepository = courseRepository;
    }

    public async Task<Result<UserCourse, UserCourseException>> Handle(CreateUserCourseCommand request, CancellationToken cancellationToken)
    {
        var courseId = new CourseId(request.CourseId);
        var userId = new UserId(request.UserId);

        var existingCourseUser = await _userCourseRepository.GetByCourseIdAndUserId(courseId, userId, cancellationToken);

        return await existingCourseUser.Match(
            f => Task.FromResult<Result<UserCourse, UserCourseException>>(new UserCourseAlreadyExistsException(f.Id)),
            async () => await CreateEntity(courseId, userId, request.Rating, request.JoinAt, request.EndAt, cancellationToken));
    }

    private async Task<Result<UserCourse, UserCourseException>> CreateEntity(
        CourseId courseId,
        UserId userId,
        int rating,
        DateTime joinAt,
        DateTime endAt,
        CancellationToken cancellationToken)
    {
        try
        {
            var userCourse = UserCourse.New(UserCourseId.New(), courseId, userId, rating, joinAt, endAt, true);
            return await _userCourseRepository.Add(userCourse, cancellationToken);
        }
        catch (Exception exception)
        {
            return new UserCourseUnknownException(UserCourseId.Empty, exception);
        }
    }
}
