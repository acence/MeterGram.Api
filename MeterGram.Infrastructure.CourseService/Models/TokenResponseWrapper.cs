using System.Text.Json.Serialization;

namespace MeterGram.Infrastructure.CourseService.Models;

public class TokenResponseWrapper
{
    [JsonPropertyName("data")]
    public TokenData Data { get; set; }
}