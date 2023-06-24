﻿namespace MeterGram.WebApi.Contracts.Responses.Project;

public class ProjectResponse
{
    /// <summary>
    /// Project Id
    /// </summary>
    public Int32 Id { get; set; }

    /// <summary>
    /// Course name
    /// </summary>
    public String CourseName { get; set; } = null!;

    /// <summary>
    /// Date
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Whether the course is active or not
    /// </summary>
    public Boolean IsActive { get; set; }
}

