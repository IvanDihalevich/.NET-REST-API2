using Domain.Courses;
using Domain.UserCourse;
using Domain.Users;
using Optional;

namespace Application.Common.Interfaces.Repositories;

public interface IUserCourseRepository
{
    Task<Option<UserCourse>> GetById(UserCourseId id, CancellationToken cancellationToken);
    Task<Option<UserCourse>> GetByCourseIdAndUserId(CourseId courseId, UserId userId, CancellationToken cancellationToken);
    Task<UserCourse> Add(UserCourse userCourse, CancellationToken cancellationToken);
    Task<UserCourse> Update(UserCourse userCourse, CancellationToken cancellationToken);
    Task<UserCourse> Delete(UserCourseId userCourseId, CancellationToken cancellationToken);
}