﻿using System.Collections;
using System.Text.Json.Serialization;

namespace MeterGram.Infrastructure.CourseService.Models;

public class CourseListWrapperResponseModel
{
    [JsonPropertyName("next_page_link")]
    public String NextPageLink { get; set; }

    [JsonPropertyName("total_count")]
    public Int32 TotalCount { get; set; }

    [JsonPropertyName("max_limit")]
    public Int32 MaxLimit { get; set; }

    [JsonPropertyName("data")]
    public IEnumerable<CourseResponseModel> Data { get; set; }
}