﻿namespace MeterGram.WebApi.Contracts.Requests.CompanyApplications
{
    public class CompanyApplicationGetRequest : PagedRequest
    {
        public string? Name { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string? Phone { get; set; } = null!;
    }
}