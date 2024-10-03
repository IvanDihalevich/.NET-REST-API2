using FluentValidation;

namespace Application.Courses.Commands;

public class DeleteCourseCommandValidator : AbstractValidator<DeleteCourseCommand>
{
    public DeleteCourseCommandValidator()
    {
        RuleFor(x => x.CourseId).NotEmpty();
    }
}