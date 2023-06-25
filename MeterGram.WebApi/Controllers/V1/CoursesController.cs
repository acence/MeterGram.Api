using AutoMapper;
using MediatR;
using MeterGram.Core.UseCases.Courses.Handlers;
using MeterGram.WebApi.Contracts.Responses;
using MeterGram.WebApi.Contracts.Responses.Course;
using MeterGram.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace MeterGram.WebApi.Controllers.V1;

/// <summary>
/// Rest API Controller that contains the methods for retrieving courses, as well as pulling them down from the external service
/// </summary>
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

    /// <summary>
    /// Returns all courses in the database
    /// </summary>
    /// <param name="onlyActive">If true, returns only active courses. If false, returns all courses</param>
    /// <returns>List of courses</returns>
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

    /// <summary>
    /// Calls the external endpoint to retrieve the courses and saves them to database
    /// </summary>
    /// <param name="shouldGetUpdatedData">Whether to retrieve the updated list</param>
    [HttpPost]
    [Route("synchronize")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IEnumerable<ValidationErrorResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ServerErrorResponse))]
    public async Task<IActionResult> Synchronize([FromQuery] Boolean shouldGetUpdatedData)
    {
        var command = new SynchronizeCourses.Command { ShouldGetUpdatedData = shouldGetUpdatedData };

        return await _mediator.SendAsync(command);
    }
}
