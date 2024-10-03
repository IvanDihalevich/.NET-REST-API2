using FluentValidation;

namespace Application.Users.Commands;

public class DeleteFacultyCommandValidator : AbstractValidator<DeleteFacultyCommand>
{
    public DeleteFacultyCommandValidator()
    {
        RuleFor(x => x.FacultyId).NotEmpty();
    }
}