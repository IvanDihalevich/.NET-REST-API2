using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Faculties.Exceptions;
using Application.Users.Exceptions;
using Domain.Faculties;
using Domain.Users;
using MediatR;

namespace Application.Users.Commands;

public record DeleteFacultyCommand : IRequest<Result<Faculty, FacultyException>>
{
    public required Guid FacultyId { get; init; }
}

public class DeleteFacultyCommandHandler(IFacultyRepository facultyRepository)
    : IRequestHandler<DeleteFacultyCommand, Result<Faculty, FacultyException>>
{
    public async Task<Result<Faculty, FacultyException>> Handle(
        DeleteFacultyCommand request,
        CancellationToken cancellationToken)
    {
        var facultyId = new FacultyId(request.FacultyId);

        var existingFaculty = await facultyRepository.GetById(facultyId, cancellationToken);

        return await existingFaculty.Match<Task<Result<Faculty, FacultyException>>>(
            async u => await DeleteEntity(u, cancellationToken),
            () => Task.FromResult<Result<Faculty, FacultyException>>(new FacultyNotFoundException(facultyId)));
    }

    public async Task<Result<Faculty, FacultyException>> DeleteEntity(Faculty faculty, CancellationToken cancellationToken)
    {
        try
        {
            return await facultyRepository.Delete(faculty, cancellationToken);
        }
        catch (Exception exception)
        {
            return new FacultyUnknownException(faculty.Id, exception);
        }
    }
}