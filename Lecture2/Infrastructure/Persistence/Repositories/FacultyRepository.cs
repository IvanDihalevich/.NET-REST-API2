using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Faculties;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class FacultyRepository(ApplicationDbContext context): IFacultyRepository, IFacultyQueries
{
    public async Task<IReadOnlyList<Faculty>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Faculties
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Option<Faculty>> SearchByName(string name, CancellationToken cancellationToken)
    {
        var entity = await context.Faculties
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == name, cancellationToken);

        return entity == null ? Option.None<Faculty>() : Option.Some(entity);
    }

    public async Task<Option<Faculty>> GetById(FacultyId id, CancellationToken cancellationToken)
    {
        var entity = await context.Faculties
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity == null ? Option.None<Faculty>() : Option.Some(entity);
    }

    public async Task<Faculty> Add(Faculty faculty, CancellationToken cancellationToken)
    {
        await context.Faculties.AddAsync(faculty, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return faculty;
    }
    
    public async Task<Faculty> Delete(Faculty faculty, CancellationToken cancellationToken)
    {
        context.Faculties.Remove(faculty);

        await context.SaveChangesAsync(cancellationToken);

        return faculty;
    }

    public async Task<Faculty> Update(Faculty faculty, CancellationToken cancellationToken)
    {
        context.Faculties.Update(faculty);

        await context.SaveChangesAsync(cancellationToken);

        return faculty;
    }
}