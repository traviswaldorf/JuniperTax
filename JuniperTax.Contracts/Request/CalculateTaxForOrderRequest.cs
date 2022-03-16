namespace JuniperTax.Contracts.Request
{
    public class CalculateTaxForOrderRequest : ApiRequest
    {
        public decimal? Amount { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}
