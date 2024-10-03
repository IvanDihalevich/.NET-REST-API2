using Domain.Faculties;

namespace Api.Dtos;

public record FacultyDto(Guid? Id, string Name)
{
    public static FacultyDto FromDomainModel(Faculty faculty)
        => new(faculty.Id.Value, faculty.Name);
}