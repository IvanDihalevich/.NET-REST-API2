using FluentValidation;

namespace Application.Faculties.Commands;

public class UpdateFacultyCommandValidator : AbstractValidator<UpdateFacultyCommand>
{
    public UpdateFacultyCommandValidator()
    {
        RuleFor(x => x.FacultyId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(255).MinimumLength(3);
    }
}