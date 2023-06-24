using System.Text.Json.Serialization;

namespace MeterGram.Infrastructure.ProjectService.Models;

public class TokenResponseWrapper
{
    [JsonPropertyName("data")]
    public TokenData Data { get; set; }
}