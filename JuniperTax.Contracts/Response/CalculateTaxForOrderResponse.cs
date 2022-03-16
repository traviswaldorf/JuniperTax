namespace JuniperTax.Contracts.Response
{
    public class CalculateTaxForOrderResponse : ApiResponse
    {
        public decimal? PreTaxAmount { get; set; }
        public decimal? TaxRate { get; set; }
        public decimal? TaxAmount { get; set; }
    }
}
