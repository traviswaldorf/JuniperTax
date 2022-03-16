using JuniperTax.TaxJar.Models;
using System.Text.Json.Serialization;

namespace JuniperTax.TaxJar.Response
{
    public class TaxForOrderResponse : TaxJarApiResponse
    {
        [JsonPropertyName("tax")]
        public Tax? Tax { get; set; }
    }
}
