using Domain.Courses;
using Domain.Users;

namespace Domain.UserCourse;

public class UserCourse
{
    public UserCourseId Id { get; }
    
    public CourseId CourseId { get; private set; }
    public Course? Course { get; private set; }
    
    public UserId UserId { get; private set;  }
    public User? User { get; private set;  } 
    
    public int Rating { get; private set;  }   
    public DateTime JoinAt { get; private set; }
    public DateTime EndAt { get; private set; }
    public bool IsJoined { get; private set; }

    private UserCourse(UserCourseId id, CourseId courseId, UserId userId, int rating,DateTime joinAt, DateTime endAt, bool isJoined)
    {
        Id = id;
        CourseId = courseId;
        UserId = userId;
        Rating = rating;
        JoinAt = joinAt;
        EndAt = endAt;
        IsJoined = isJoined;
        
    }
    
    public static UserCourse New(UserCourseId id, CourseId courseId, UserId userId, int rating, DateTime joinAt, DateTime endAt, bool isJoined)
        => new(id, courseId, userId, rating, joinAt, endAt, isJoined);
    
    public void UpdateDetails(CourseId courseId, UserId userId, int rating, DateTime joinAt, DateTime endAt)
    {
        CourseId = courseId;
        UserId = userId;
        Rating = rating;
        JoinAt = joinAt;
        EndAt = endAt;
    }
    
    public void EndCourseForAll()
    {
        IsJoined = false;
    }
    
    public void UpdateRatingForEnd(int rating)
    {
        Rating = rating;
        EndAt = DateTime.UtcNow;
        IsJoined = false;
    }
}