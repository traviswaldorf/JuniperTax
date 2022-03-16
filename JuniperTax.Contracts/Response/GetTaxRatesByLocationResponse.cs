using System;

namespace JuniperTax.Contracts.Response
{
    public class GetTaxRatesByLocationResponse : ApiResponse
    {
        public decimal? Rate { get; set; }
    }
}
