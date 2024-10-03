using Domain.Courses;
using Domain.Faculties;

namespace Api.Dtos;

public record CourseDto(Guid? Id, string Name,DateTime StartAt,DateTime FinishAt,string MaxStudents, DateTime? UpdatedAt)
{
    public static CourseDto FromDomainModel(Course course)
        => new(
            Id: course.Id.Value,
            Name: course.Name,
            StartAt: course.StartAt,
            FinishAt: course.FinishAt,
            MaxStudents: course.MaxStudents,
            UpdatedAt: course.UpdatedAt
        );
}