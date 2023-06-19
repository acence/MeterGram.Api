using AutoMapper;
using MediatR;
using Metergram.Core.UseCases.Projects.Handlers;
using MeterGram.WebApi.Contracts.Responses;
using MeterGram.WebApi.Contracts.Responses.Project;
using MeterGram.WebApi.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeterGram.WebApi.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProjectsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProjectResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IEnumerable<ValidationErrorResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ServerErrorResponse))]
        public async Task<IActionResult> GetAll([FromQuery] Boolean onlyActive)
        {
            var query = new GetAllProjects.Query { OnlyActive = onlyActive };

            return await _mediator.SendAndProcessResponseAsync<GetAllProjects.Query, IList<ProjectResponse>>(_mapper, query);
        }

        [HttpGet]
        [Route("synchronize")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProjectResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IEnumerable<ValidationErrorResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ServerErrorResponse))]
        public async Task<IActionResult> Synchronize([FromQuery] Boolean shouldGetUpdatedData)
        {
            var query = new SynchronizeProjects.Query { ShouldGetUpdatedData = shouldGetUpdatedData };

            return await _mediator.SendAsync<SynchronizeProjects.Query>(query);
        }
    }
}
