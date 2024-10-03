using Domain.Courses;
using Domain.UserCourse;
using Domain.Users;
using Optional;

namespace Application.Common.Interfaces.Queries;

public interface IUserCourseQueries
{
    Task<IReadOnlyList<UserCourse>> GetAll(CancellationToken cancellationToken);
    Task<Option<UserCourse>> GetById(UserCourseId id, CancellationToken cancellationToken);
    Task<IReadOnlyList<UserCourse>> GetByUserId(UserId userId, CancellationToken cancellationToken);
    Task<IReadOnlyList<UserCourse>> GetByCourseId(CourseId courseId, CancellationToken cancellationToken);
}