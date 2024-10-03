using FluentValidation;

namespace Application.UserCourses.Commands;

public class DeleteUserCourseCommandValidator : AbstractValidator<DeleteUserCourseCommand>
{
    public DeleteUserCourseCommandValidator()
    {
        RuleFor(x => x.UserCourseId).NotEmpty();
    }
}