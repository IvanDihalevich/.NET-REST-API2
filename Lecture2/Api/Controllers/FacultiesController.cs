using Api.Dtos;
using Api.Modules.Errors;
using Application.Common.Interfaces.Queries;
using Application.Faculties.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("faculties")]
[ApiController]
public class FacultiesController(ISender sender, IFacultyQueries facultyQueries) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<FacultyDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await facultyQueries.GetAll(cancellationToken);

        return entities.Select(FacultyDto.FromDomainModel).ToList();
    }

    [HttpPost]
    public async Task<ActionResult<FacultyDto>> Create(
        [FromBody] FacultyDto request,
        CancellationToken cancellationToken)
    {
        var input = new CreateFacultyCommand
        {
            Name = request.Name
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<FacultyDto>>(
            f => FacultyDto.FromDomainModel(f),
            e => e.ToObjectResult());
    }

    [HttpPut]
    public async Task<ActionResult<FacultyDto>> Update(
        [FromBody] FacultyDto request,
        CancellationToken cancellationToken)
    {
        var input = new UpdateFacultyCommand
        {
            FacultyId = request.Id!.Value,
            Name = request.Name
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<FacultyDto>>(
            f => FacultyDto.FromDomainModel(f),
            e => e.ToObjectResult());
    }
}