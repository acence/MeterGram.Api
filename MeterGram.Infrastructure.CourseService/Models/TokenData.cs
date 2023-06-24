using System.Text.Json.Serialization;

namespace MeterGram.Infrastructure.CourseService.Models;

public class TokenData
{
    [JsonPropertyName("accessToken")]
    public String AccessToken { get; set; } = null;
}