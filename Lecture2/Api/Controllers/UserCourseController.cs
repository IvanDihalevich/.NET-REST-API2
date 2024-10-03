using Api.Dtos;
using Application.Common.Interfaces.Queries;
using Application.UserCourses.Commands;
using Domain.UserCourse;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("user-courses")]
[ApiController]
public class UserCoursesController(ISender sender, IUserCourseQueries userCourseQueries) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<UserCourseDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await userCourseQueries.GetAll(cancellationToken);
        return entities.Select(UserCourseDto.FromDomainModel).ToList();
    }

    [HttpGet("{userCourseId:guid}")]
    public async Task<ActionResult<UserCourseDto>> Get([FromRoute] Guid userCourseId, CancellationToken cancellationToken)
    {
        var entity = await userCourseQueries.GetById(new UserCourseId(userCourseId), cancellationToken);
        return entity.Match<ActionResult<UserCourseDto>>(
            uc => UserCourseDto.FromDomainModel(uc),
            () => NotFound());
    }

    [HttpPost]
    public async Task<ActionResult<UserCourseDto>> Create([FromBody] UserCourseDto request, CancellationToken cancellationToken)
    {
        if (request.JoinAt == null || request.EndAt == null)
        {
            return BadRequest("JoinAt and EndAt cannot be null.");
        }

        var input = new CreateUserCourseCommand
        {
            CourseId = request.CourseId,
            UserId = request.UserId,
            Rating = request.Rating,
            JoinAt = request.JoinAt,
            EndAt = request.EndAt.Value 
        };

        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<UserCourseDto>>(
            uc => UserCourseDto.FromDomainModel(uc),
            e => e.ToObjectResult());
    }

    [HttpPut]
    public async Task<ActionResult<UserCourseDto>> Update([FromBody] UserCourseDto request, CancellationToken cancellationToken)
    {
        if (request.JoinAt == null || request.EndAt == null)
        {
            return BadRequest("JoinAt and EndAt cannot be null.");
        }

        var input = new UpdateUserCourseCommand
        {
            Id = request.Id!.Value,
            CourseId = request.CourseId,
            UserId = request.UserId,
            Raiting = request.Rating,
            JoinAt = request.JoinAt,
            EndAt = request.EndAt.Value 
        };

        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<UserCourseDto>>(
            uc => UserCourseDto.FromDomainModel(uc),
            e => e.ToObjectResult());
    }


    [HttpDelete("{userCourseId:guid}")]
    public async Task<ActionResult<UserCourseDto>> Delete([FromRoute] Guid userCourseId, CancellationToken cancellationToken)
    {
        var input = new DeleteUserCourseCommand
        {
            UserCourseId = userCourseId
        };

        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<UserCourseDto>>(
            uc => UserCourseDto.FromDomainModel(uc),
            e => e.ToObjectResult());
    }
}
