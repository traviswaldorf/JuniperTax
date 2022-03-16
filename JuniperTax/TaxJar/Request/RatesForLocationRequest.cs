using System.Text.Json.Serialization;

namespace JuniperTax.TaxJar.Request
{
    public class RatesForLocationRequest
    {
        public string? Zip { get; set; }
        public RatesForLocationRequestParameters? RatesForLocationRequestParameters { get; set; }
    }

    public class RatesForLocationRequestParameters
    {
        [JsonPropertyName("country")]
        public string? Country;

        [JsonPropertyName("state")]
        public string? State;

        [JsonPropertyName("city")]
        public string? City;

        [JsonPropertyName("street")]
        public string? Street;
    }
}
