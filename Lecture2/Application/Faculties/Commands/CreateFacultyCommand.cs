using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Faculties.Exceptions;
using Domain.Faculties;
using MediatR;

namespace Application.Faculties.Commands;

public record CreateFacultyCommand : IRequest<Result<Faculty, FacultyException>>
{
    public required string Name { get; init; }
}

public class CreateFacultyCommandHandler(
    IFacultyRepository facultyRepository) : IRequestHandler<CreateFacultyCommand, Result<Faculty, FacultyException>>
{
    public async Task<Result<Faculty, FacultyException>> Handle(
        CreateFacultyCommand request,
        CancellationToken cancellationToken)
    {
        var existingFaculty = await facultyRepository.SearchByName(request.Name, cancellationToken);

        return await existingFaculty.Match(
            f => Task.FromResult<Result<Faculty, FacultyException>>(new FacultyAlreadyExistsException(f.Id)),
            async () => await CreateEntity(request.Name, cancellationToken));
    }

    private async Task<Result<Faculty, FacultyException>> CreateEntity(
        string name,
        CancellationToken cancellationToken)
    {
        try
        {
            var entity = Faculty.New(FacultyId.New(), name);

            return await facultyRepository.Add(entity, cancellationToken);
        }
        catch (Exception exception)
        {
            return new FacultyUnknownException(FacultyId.Empty, exception);
        }
    }
}