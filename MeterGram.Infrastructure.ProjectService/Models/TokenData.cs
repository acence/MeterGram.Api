using System.Text.Json.Serialization;

namespace MeterGram.Infrastructure.ProjectService.Models;

public class TokenData
{
    [JsonPropertyName("accessToken")]
    public String AccessToken { get; set; } = null;
}