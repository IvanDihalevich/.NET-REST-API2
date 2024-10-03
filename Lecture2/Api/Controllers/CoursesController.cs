using Api.Dtos;
using Api.Modules.Errors;
using Application.Common.Interfaces.Queries;
using Application.Courses.Commands;
using Domain.Courses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("courses")]
[ApiController]
public class CoursesController(ISender sender, ICourseQueries courseQueries) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CourseDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await courseQueries.GetAll(cancellationToken);

        return entities.Select(CourseDto.FromDomainModel).ToList();
    }
    
    [HttpGet("{courseId:guid}")]
    public async Task<ActionResult<CourseDto>> Get([FromRoute] Guid courseId, CancellationToken cancellationToken)
    {
        var entity = await courseQueries.GetById(new CourseId(courseId), cancellationToken);

        return entity.Match<ActionResult<CourseDto>>(
            u => CourseDto.FromDomainModel(u),
            () => NotFound());
    }

    [HttpPost]
    public async Task<ActionResult<CourseDto>> Create(
        [FromBody] CourseDto request,
        CancellationToken cancellationToken)
    {
        var input = new CreateCourseCommand
        {
            Name = request.Name,
            StartAt = request.StartAt,
            FinishAt = request.FinishAt,
            MaxStudents = request.MaxStudents,
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<CourseDto>>(
            f => CourseDto.FromDomainModel(f),
            e => e.ToObjectResult());
    }
    
    [HttpDelete("{courseId:guid}")]
    public async Task<ActionResult<CourseDto>> Delete([FromRoute] Guid courseId, CancellationToken cancellationToken)
    {
        var input = new DeleteCourseCommand
        {
            CourseId = courseId
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<CourseDto>>(
            u => CourseDto.FromDomainModel(u),
            e => e.ToObjectResult());
    }
    
    [HttpPut]
    public async Task<ActionResult<CourseDto>> Update(
        [FromBody] CourseDto request,
        CancellationToken cancellationToken)
    {
        var input = new UpdateCourseCommand
        {
            Name = request.Name,
            CourseId = request.Id!.Value,
            StartAt = request.StartAt,
            FinishAt = request.FinishAt,
            MaxStudents = request.MaxStudents,
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<CourseDto>>(
            f => CourseDto.FromDomainModel(f),
            e => e.ToObjectResult());
    }
}