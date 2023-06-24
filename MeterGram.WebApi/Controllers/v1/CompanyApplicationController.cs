﻿using AutoMapper;
using MediatR;
using MeterGram.Core.UseCases.Applications.UseCases;
using MeterGram.WebApi.Contracts.Requests.CompanyApplications;
using MeterGram.WebApi.Contracts.Responses;
using MeterGram.WebApi.Contracts.Responses.CompanyApplications;
using MeterGram.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace MeterGram.WebApi.Controllers.V1;

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
