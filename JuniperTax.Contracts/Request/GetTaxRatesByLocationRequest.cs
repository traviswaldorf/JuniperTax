namespace JuniperTax.Contracts.Request
{
    public class GetTaxRatesByLocationRequest : ApiRequest
    {
        public string Country { get; set; }
        public string Zip { get; set; }
    }
}
