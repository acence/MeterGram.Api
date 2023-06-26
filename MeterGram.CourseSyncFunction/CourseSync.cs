using System;
using System.IO;
using System.Threading.Tasks;
using MediatR;
using MeterGram.Core.UseCases.Courses.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MeterGram.CourseSyncFunction;

public class CourseSync
{
    private readonly IMediator _mediator;

    public CourseSync(IMediator mediator)
    {
        _mediator = mediator;
    }

    [FunctionName("SynchronizeCourses")]
    public async Task Run([TimerTrigger("* * 0 * * *", RunOnStartup = true)]TimerInfo timer, ILogger log)
    {
        log.LogInformation($"Course synchronization started at: {DateTime.Now}");

        await _mediator.Send(new SynchronizeCourses.Command
        {
            ShouldGetUpdatedData = true,
        }).ConfigureAwait(false);
    }
}
