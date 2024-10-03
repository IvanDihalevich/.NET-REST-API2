using FluentValidation;

namespace Application.UserCourses.Commands;

public class CreateUserCourseCommandValidator : AbstractValidator<CreateUserCourseCommand>
{
    public CreateUserCourseCommandValidator()
    {
        RuleFor(x => x.CourseId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Rating).InclusiveBetween(0, 100); 
        RuleFor(x => x.JoinAt).NotEmpty();
        RuleFor(x => x.EndAt).NotEmpty();
    }
}