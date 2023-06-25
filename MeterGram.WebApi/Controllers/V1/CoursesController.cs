using AutoMapper;
using MediatR;
using MeterGram.Core.UseCases.Courses.Handlers;
using MeterGram.WebApi.Contracts.Responses;
using MeterGram.WebApi.Contracts.Responses.Course;
using MeterGram.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace MeterGram.WebApi.Controllers.V1;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/courses")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CoursesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CourseResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IEnumerable<ValidationErrorResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ServerErrorResponse))]
    public async Task<IActionResult> GetAll([FromQuery] Boolean onlyActive = true)
    {
        var query = new GetAllCourses.Query { OnlyActive = onlyActive };

        return await _mediator.SendAndProcessResponseAsync<GetAllCourses.Query, IList<CourseResponse>>(_mapper, query);
    }

    [HttpPost]
    [Route("synchronize")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CourseResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IEnumerable<ValidationErrorResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ServerErrorResponse))]
    public async Task<IActionResult> Synchronize([FromQuery] Boolean shouldGetUpdatedData)
    {
        var command = new SynchronizeCourses.Command { ShouldGetUpdatedData = shouldGetUpdatedData };

        return await _mediator.SendAsync(command);
    }
}
