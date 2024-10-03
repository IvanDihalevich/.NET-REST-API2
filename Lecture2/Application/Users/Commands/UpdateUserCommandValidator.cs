using FluentValidation;

namespace Application.Users.Commands;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(255).MinimumLength(2);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(255).MinimumLength(2);
        RuleFor(x => x.UserId).NotEmpty();
    }
}