using AutoMapper;
using MediatR;
using Metergram.Core.UseCases.Projects.Handlers;
using MeterGram.WebApi.Contracts.Responses.Project;
using MeterGram.WebApi.Contracts.Responses;
using MeterGram.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;
using MeterGram.WebApi.Contracts.Responses.CompanyApplications;
using MeterGram.Domain.Models;
using MeterGram.WebApi.Contracts.Requests.CompanyApplications;
using Metergram.Core.UseCases.Applications.UseCases;

namespace MeterGram.WebApi.Controllers.v1
{
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
}
