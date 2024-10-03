using FluentValidation;

namespace Application.Users.Commands;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(255).MinimumLength(3);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(255).MinimumLength(3);
        RuleFor(x => x.FacultyId).NotEmpty();
    }
}