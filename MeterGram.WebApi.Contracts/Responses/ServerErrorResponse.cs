using System.ComponentModel.DataAnnotations;

namespace MeterGram.WebApi.Contracts.Responses;

public class ServerErrorResponse
{
    /// <summary>
    /// Exception message
    /// </summary>
    [Required]
    public string Message { get; set; } = null!;
}
