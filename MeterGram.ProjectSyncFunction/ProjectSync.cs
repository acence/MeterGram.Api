using System;
using System.Threading.Tasks;
using MediatR;
using MeterGram.Core.UseCases.Projects.Handlers;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace MeterGram.ProjectSyncFunction;

public class ProjectSync
{
    private readonly IMediator _mediator;

    public ProjectSync(IMediator mediator)
    {
        _mediator = mediator;
    }
    [FunctionName("SynchronizeProjects")]
    public async Task Run([TimerTrigger("* * 0 * * *")]TimerInfo myTimer, ILogger log)
    {
        log.LogInformation($"Project synchronization started at: {DateTime.Now}");

        await _mediator.Send(new SynchronizeProjects.Query
        {
            ShouldGetUpdatedData = true,
        }).ConfigureAwait(false);
    }
}
