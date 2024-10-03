using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.UserCourses.Exceptions;
using Domain.UserCourse;
using MediatR;

namespace Application.UserCourses.Commands;

public record DeleteUserCourseCommand : IRequest<Result<UserCourse, UserCourseException>>
{
    public required Guid UserCourseId { get; init; }
}

public class DeleteUserCourseCommandHandler : IRequestHandler<DeleteUserCourseCommand, Result<UserCourse, UserCourseException>>
{
    private readonly IUserCourseRepository _userCourseRepository;

    public DeleteUserCourseCommandHandler(IUserCourseRepository userCourseRepository)
    {
        _userCourseRepository = userCourseRepository;
    }

    public async Task<Result<UserCourse, UserCourseException>> Handle(
        DeleteUserCourseCommand request,
        CancellationToken cancellationToken)
    {
        var userCourseId = new UserCourseId(request.UserCourseId);

        var existingUserCourse = await _userCourseRepository.GetById(userCourseId, cancellationToken);

        return await existingUserCourse.Match<Task<Result<UserCourse, UserCourseException>>>( 
            async uc => await DeleteEntity(uc, cancellationToken),
            () => Task.FromResult<Result<UserCourse, UserCourseException>>(new UserCourseNotFoundException(userCourseId)));
    }

    private async Task<Result<UserCourse, UserCourseException>> DeleteEntity(UserCourse userCourse, CancellationToken cancellationToken)
    {
        try
        {
            return await _userCourseRepository.Delete(userCourse.Id, cancellationToken);
        }
        catch (Exception exception)
        {
            return new UserCourseUnknownException(userCourse.Id, exception);
        }
    }
}