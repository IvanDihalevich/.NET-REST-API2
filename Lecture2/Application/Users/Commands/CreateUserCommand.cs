using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Users.Exceptions;
using Domain.Faculties;
using Domain.Users;
using MediatR;

namespace Application.Users.Commands;

public record CreateUserCommand : IRequest<Result<User, UserException>>
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required Guid FacultyId { get; init; }
}

public class CreateUserCommandHandler(
    IUserRepository userRepository,
    IFacultyRepository facultyRepository)
    : IRequestHandler<CreateUserCommand, Result<User, UserException>>
{
    public async Task<Result<User, UserException>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var facultyId = new FacultyId(request.FacultyId);

        var faculty = await facultyRepository.GetById(facultyId, cancellationToken);

        return await faculty.Match<Task<Result<User, UserException>>>(
            async f =>
            {
                var existingUser = await userRepository.GetByFirstNameAndLastName(
                    request.FirstName,
                    request.LastName,
                    cancellationToken);

                return await existingUser.Match(
                    u => Task.FromResult<Result<User, UserException>>(new UserAlreadyExistsException(u.Id)),
                    async () => await CreateEntity(request.FirstName, request.LastName, f.Id, cancellationToken));
            },
            () => Task.FromResult<Result<User, UserException>>(new UserFacultyNotFoundException(facultyId)));
    }

    private async Task<Result<User, UserException>> CreateEntity(
        string firstName,
        string lastName,
        FacultyId facultyId,
        CancellationToken cancellationToken)
    {
        try
        {
            var entity = User.New(UserId.New(), firstName, lastName, facultyId);

            return await userRepository.Add(entity, cancellationToken);
        }
        catch (Exception exception)
        {
            return new UserUnknownException(UserId.Empty(), exception);
        }
    }
}