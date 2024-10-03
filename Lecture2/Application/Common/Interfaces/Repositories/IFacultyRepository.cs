using Domain.Faculties;
using Optional;

namespace Application.Common.Interfaces.Repositories;

public interface IFacultyRepository
{
    Task<Option<Faculty>> GetById(FacultyId id, CancellationToken cancellationToken);
    Task<Option<Faculty>> SearchByName(string name, CancellationToken cancellationToken);
    Task<Faculty> Add(Faculty faculty, CancellationToken cancellationToken);
    Task<Faculty> Delete(Faculty faculty, CancellationToken cancellationToken);
    Task<Faculty> Update(Faculty faculty, CancellationToken cancellationToken);
}