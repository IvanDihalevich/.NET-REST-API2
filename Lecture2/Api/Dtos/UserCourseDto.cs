

using Domain.UserCourse;

namespace Api.Dtos;

public record UserCourseDto(
    Guid? Id,
    Guid CourseId,
    Guid UserId,
    int Rating,
    DateTime JoinAt,
    DateTime? EndAt,
    bool IsJoined)
{
    public static UserCourseDto FromDomainModel(UserCourse userCourse)
        => new(
            Id: userCourse.Id.Value,
            CourseId: userCourse.CourseId.Value,
            UserId: userCourse.UserId.Value,
            Rating: userCourse.Rating,
            JoinAt: userCourse.JoinAt,
            EndAt: userCourse.EndAt,
            IsJoined: userCourse.IsJoined);
}