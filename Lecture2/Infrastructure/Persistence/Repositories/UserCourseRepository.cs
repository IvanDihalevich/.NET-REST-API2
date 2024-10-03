using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Application.UserCourses.Exceptions;
using Domain.Courses;
using Domain.UserCourse;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class UserCourseRepository(ApplicationDbContext context) : IUserCourseRepository, IUserCourseQueries
{
    public async Task<Option<UserCourse>> GetById(UserCourseId id, CancellationToken cancellationToken)
    {
        return await context.UserCourse 
            .AsNoTracking()
            .FirstOrDefaultAsync(uc => uc.Id == id, cancellationToken)
            .ContinueWith(task => task.Result != null ? Option.Some(task.Result) : Option.None<UserCourse>(), cancellationToken);
    }

    public async Task<Option<UserCourse>> GetByCourseIdAndUserId(CourseId courseId, UserId userId, CancellationToken cancellationToken)
    {
        return await context.UserCourse 
            .AsNoTracking()
            .FirstOrDefaultAsync(uc => uc.CourseId == courseId && uc.UserId == userId, cancellationToken)
            .ContinueWith(task => task.Result != null ? Option.Some(task.Result) : Option.None<UserCourse>(), cancellationToken);
    }

    public async Task<UserCourse> Add(UserCourse userCourse, CancellationToken cancellationToken)
    {
        try
        {
            await context.UserCourse.AddAsync(userCourse, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return userCourse; 
        }
        catch (Exception ex)
        {
            throw new UserCourseUnknownException(UserCourseId.Empty, ex);
        }
    }

    public async Task<UserCourse> Update(UserCourse userCourse, CancellationToken cancellationToken)
    {
        try
        {
            context.UserCourse.Update(userCourse);
            await context.SaveChangesAsync(cancellationToken);
            return userCourse; 
        }
        catch (Exception ex)
        {
            throw new UserCourseUnknownException(userCourse.Id, ex);
        }
    }

    public async Task<UserCourse> Delete(UserCourseId userCourseId, CancellationToken cancellationToken)
    {
        var userCourse = await GetById(userCourseId, cancellationToken);

        return await userCourse.Match(
            async uc =>
            {
                context.UserCourse.Remove(uc);
                await context.SaveChangesAsync(cancellationToken);
                return uc; 
            },
            () => throw new UserCourseNotFoundException(userCourseId));
    }
    
    public async Task<IReadOnlyList<UserCourse>> GetAll(CancellationToken cancellationToken)
    {
        return await context.UserCourse
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<UserCourse>> GetByUserId(UserId userId, CancellationToken cancellationToken)
    {
        return await context.UserCourse
            .AsNoTracking()
            .Where(uc => uc.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<UserCourse>> GetByCourseId(CourseId courseId, CancellationToken cancellationToken)
    {
        return await context.UserCourse
            .AsNoTracking()
            .Where(uc => uc.CourseId == courseId)
            .ToListAsync(cancellationToken);
    }
}
