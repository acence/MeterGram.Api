﻿namespace MeterGram.WebApi.Contracts.Responses.CompanyApplications;

public class CompanyApplicationResponseCourse
{
    public Int32 Id { get; set; }
    public String CourseName { get; set; } = null!;
    public DateTime Date { get; set; }
}