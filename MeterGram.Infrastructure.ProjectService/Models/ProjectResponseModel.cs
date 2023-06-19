﻿using System.Text.Json.Serialization;

namespace MeterGram.Infrastructure.ProjectService.Models
{
    public class ProjectResponseModel
    {
        [JsonPropertyName("id")]
        public Int32 Id { get; set; }

        [JsonPropertyName("course_name")]
        public String CourseName { get; set; } = null;

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("is_active")]
        public Boolean IsActive { get; set; }
    }
}