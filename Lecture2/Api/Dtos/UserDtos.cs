using Domain.Faculties;
using Domain.Users;

namespace Api.Dtos;

public record UserDto(
    Guid? Id,
    string FirstName,
    string LastName,
    string? FullName,
    DateTime? UpdatedAt,
    Guid FacultyId,
    FacultyDto? Faculty)
{
    public static UserDto FromDomainModel(User user)
        => new(
            Id: user.Id.Value,
            FirstName: user.FirstName,
            LastName: user.LastName,
            FullName: user.FullName,
            UpdatedAt: user.UpdatedAt,
            FacultyId: user.FacultyId.Value,
            Faculty: user.Faculty == null ? null : FacultyDto.FromDomainModel(user.Faculty));
}
