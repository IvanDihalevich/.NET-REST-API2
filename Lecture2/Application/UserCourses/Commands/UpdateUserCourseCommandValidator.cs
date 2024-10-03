using FluentValidation;

namespace Application.UserCourses.Commands;

public class UpdateUserCourseCommandValidator : AbstractValidator<UpdateUserCourseCommand>
{
    public UpdateUserCourseCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.CourseId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Raiting).InclusiveBetween(0, 100);
        RuleFor(x => x.JoinAt).NotEmpty();
        RuleFor(x => x.EndAt).NotEmpty();
    }
}