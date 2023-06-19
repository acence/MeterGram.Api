using System.Text.Json.Serialization;

namespace MeterGram.Infrastructure.ProjectService.Models
{
    public class ProjectListWrapperResponseModel
    {
        [JsonPropertyName("next_page_link")]
        public String NextPageLink { get; set; }

        [JsonPropertyName("total_count")]
        public Int32 TotalCount { get; set; }

        [JsonPropertyName("max_limit")]
        public Int32 MaxLimit { get; set; }

        public String Data { get; set; }
    }
}