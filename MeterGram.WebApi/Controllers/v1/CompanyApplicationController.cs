using AutoMapper;
using MediatR;
using MeterGram.Core.UseCases.Applications.Handlers;
using MeterGram.WebApi.Contracts.Requests.CompanyApplications;
using MeterGram.WebApi.Contracts.Responses;
using MeterGram.WebApi.Contracts.Responses.CompanyApplications;
using MeterGram.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace MeterGram.WebApi.Controllers.V1;

/// <summary>
/// Rest API Controller that contains the methods for retrieving applications for courses, as well as applying to them
/// </summary>
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/company-applications")]
[ApiController]
public class CompanyApplicationController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CompanyApplicationController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Get all applications filtered by specific company values
    /// </summary>
    /// <param name="request">Filter parameters for retrieving the applications</param>
    /// <returns>A page-wrapped list of company applications</returns>
    [HttpGet]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<CompanyApplicationResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IEnumerable<ValidationErrorResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ServerErrorResponse))]
    public async Task<IActionResult> GetAll([FromQuery] CompanyApplicationGetRequest request)
    {
        var query = _mapper.Map<GetAllApplications.Query>(request);

        return await _mediator.SendAndProcessResponseAsync<GetAllApplications.Query, PagedResponse<CompanyApplicationResponse>>(_mapper, query);
    }

    /// <summary>
    /// Get all applications filtered by specific participant values across multiple companies
    /// </summary>
    /// <param name="request">Filter parameters for retrieving the applications</param>
    /// <returns>A page-wrapped list of company applications</returns>
    [HttpGet]
    [Route("participant")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<CompanyApplicationResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IEnumerable<ValidationErrorResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ServerErrorResponse))]
    public async Task<IActionResult> GetAllForParticipant([FromQuery] CompanyApplicationGetForParticipantRequest request)
    {
        var query = _mapper.Map<GetAllApplicationsForParticipant.Query>(request);

        return await _mediator.SendAndProcessResponseAsync<GetAllApplicationsForParticipant.Query, PagedResponse<CompanyApplicationResponse>>(_mapper, query);
    }

    /// <summary>
    /// Applies for a course
    /// </summary>
    /// <param name="request">The required and optional data for participation</param>
    /// <returns>The newly created application</returns>
    [HttpPost]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompanyApplicationResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IEnumerable<ValidationErrorResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ServerErrorResponse))]
    public async Task<IActionResult> Create([FromBody] CompanyApplicationCreateRequest request)
    {
        var command = _mapper.Map<CreateNewApplication.Command>(request);

        return await _mediator.SendAndProcessResponseAsync<CreateNewApplication.Command, CompanyApplicationResponse>(_mapper, command);
    }
}
