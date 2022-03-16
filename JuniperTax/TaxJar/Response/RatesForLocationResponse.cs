using JuniperTax.TaxJar.Models;
using System.Text.Json.Serialization;

namespace JuniperTax.TaxJar.Response
{
    public class RatesForLocationResponse : TaxJarApiResponse
    {
        [JsonPropertyName("rate")]
        public Rate? Rate { get; set; }
    }
}
