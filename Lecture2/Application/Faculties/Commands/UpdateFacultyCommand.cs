using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Faculties.Exceptions;
using Domain.Faculties;
using MediatR;
using Optional;

namespace Application.Faculties.Commands;

public record UpdateFacultyCommand : IRequest<Result<Faculty, FacultyException>>
{
    public required Guid FacultyId { get; init; }
    public required string Name { get; init; }
}

public class UpdateFacultyCommandHandler(
    IFacultyRepository facultyRepository) : IRequestHandler<UpdateFacultyCommand, Result<Faculty, FacultyException>>
{
    public async Task<Result<Faculty, FacultyException>> Handle(
        UpdateFacultyCommand request,
        CancellationToken cancellationToken)
    {
        var facultyId = new FacultyId(request.FacultyId);
        var faculty = await facultyRepository.GetById(facultyId, cancellationToken);

        return await faculty.Match(
            async f =>
            {
                var existingFaculty = await CheckDuplicated(facultyId, request.Name, cancellationToken);

                return await existingFaculty.Match(
                    ef => Task.FromResult<Result<Faculty, FacultyException>>(new FacultyAlreadyExistsException(ef.Id)),
                    async () => await UpdateEntity(f, request.Name, cancellationToken));
            },
            () => Task.FromResult<Result<Faculty, FacultyException>>(new FacultyNotFoundException(facultyId)));
    }

    private async Task<Result<Faculty, FacultyException>> UpdateEntity(
        Faculty faculty,
        string name,
        CancellationToken cancellationToken)
    {
        try
        {
            faculty.UpdateDetails(name);

            return await facultyRepository.Update(faculty, cancellationToken);
        }
        catch (Exception exception)
        {
            return new FacultyUnknownException(faculty.Id, exception);
        }
    }

    private async Task<Option<Faculty>> CheckDuplicated(
        FacultyId facultyId,
        string name,
        CancellationToken cancellationToken)
    {
        var faculty = await facultyRepository.SearchByName(name, cancellationToken);

        return faculty.Match(
            f => f.Id == facultyId ? Option.None<Faculty>() : Option.Some(f),
            Option.None<Faculty>);
    }
}