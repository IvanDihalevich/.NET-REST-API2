using FluentValidation;

namespace Application.Courses.Commands;

public class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
{
    public UpdateCourseCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(255)
            .MinimumLength(3);

        RuleFor(x => x.MaxStudents)
            .NotEmpty()
            .MaximumLength(255)
            .MinimumLength(3);

        RuleFor(x => x.StartAt)
            .NotEmpty()
            .Must(BeUtc).WithMessage("StartAt must be in UTC.")
            .LessThan(x => x.FinishAt).WithMessage("StartAt must be earlier than FinishAt.");

        RuleFor(x => x.FinishAt)
            .NotEmpty()
            .Must(BeUtc).WithMessage("FinishAt must be in UTC.");
        
    }
    private bool BeUtc(DateTime dateTime)
    {
        return dateTime.Kind == DateTimeKind.Utc;
    }
}