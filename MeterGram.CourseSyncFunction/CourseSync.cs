using System;
using System.Threading.Tasks;
using MediatR;
using MeterGram.Core.UseCases.Courses.Handlers;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace MeterGram.CourseSyncFunction;

public class CourseSync
{
    private readonly IMediator _mediator;

    public CourseSync(IMediator mediator)
    {
        _mediator = mediator;
    }
    [FunctionName("SynchronizeCourses")]
    public async Task Run([TimerTrigger("* * 0 * * *")]TimerInfo myTimer, ILogger log)
    {
        log.LogInformation($"Course synchronization started at: {DateTime.Now}");

        await _mediator.Send(new SynchronizeCourses.Query
        {
            ShouldGetUpdatedData = true,
        }).ConfigureAwait(false);
    }
}
