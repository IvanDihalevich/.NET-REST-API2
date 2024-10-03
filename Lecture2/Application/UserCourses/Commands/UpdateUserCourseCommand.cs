using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.UserCourses.Exceptions;
using Domain.Courses;
using Domain.UserCourse;
using Domain.Users;
using MediatR;

namespace Application.UserCourses.Commands;

public record UpdateUserCourseCommand : IRequest<Result<UserCourse, UserCourseException>>
{
    public required Guid Id { get; init; }
    public required Guid CourseId { get; init; }
    public required Guid UserId { get; init; }
    public required int Raiting { get; init; }
    public required DateTime JoinAt { get; init; }
    public required DateTime EndAt { get; init; }
}

public class UpdateUserCourseCommandHandler : IRequestHandler<UpdateUserCourseCommand, Result<UserCourse, UserCourseException>>
{
    private readonly IUserCourseRepository _userCourseRepository;

    public UpdateUserCourseCommandHandler(IUserCourseRepository userCourseRepository)
    {
        _userCourseRepository = userCourseRepository;
    }

    public async Task<Result<UserCourse, UserCourseException>> Handle(UpdateUserCourseCommand request, CancellationToken cancellationToken)
    {
        var userCourseId = new UserCourseId(request.Id);
        var userCourseOption = await _userCourseRepository.GetById(userCourseId, cancellationToken);

        return await userCourseOption.Match<Task<Result<UserCourse, UserCourseException>>>(
            async userCourse =>
            {
                userCourse.UpdateDetails(
                    new CourseId(request.CourseId),
                    new UserId(request.UserId),
                    request.Raiting,
                    request.JoinAt,
                    request.EndAt);

                return await _userCourseRepository.Update(userCourse, cancellationToken);
            },
            () => Task.FromResult<Result<UserCourse, UserCourseException>>(new UserCourseNotFoundException(userCourseId)));
    }
}