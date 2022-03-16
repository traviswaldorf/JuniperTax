using System.Text.Json.Serialization;

namespace JuniperTax.TaxJar.Response
{
    public class TaxJarApiResponse
    {
        [JsonPropertyName("status")]
        public int? Status { get; set; }

        [JsonPropertyName("error")]
        public string? Error { get; set; }

        [JsonPropertyName("detail")]
        public string? Detail { get; set; }
    }
}
