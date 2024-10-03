using Domain.Faculties;

namespace Application.Common.Interfaces.Queries;

public interface IFacultyQueries
{
    Task<IReadOnlyList<Faculty>> GetAll(CancellationToken cancellationToken);
}