using JuniperTax.Contracts.Request;
using JuniperTax.Contracts.Response;
using JuniperTax.TaxFake.Request;
using JuniperTax.TaxMock;

namespace JuniperTax.Services.Strategy.Strategies
{
    public class TaxFakeTaxStrategy : ITaxStrategy
    {
        private readonly ITaxFakeClient _taxFakeClient;

        public TaxFakeTaxStrategy(ITaxFakeClient taxFakeClient)
        {
            _taxFakeClient = taxFakeClient;
        }

        public async Task<GetTaxRatesByLocationResponse> GetTaxRatesByLocation(GetTaxRatesByLocationRequest request)
        {
            var response = new GetTaxRatesByLocationResponse();

            var getTaxRateResponse = await _taxFakeClient.GetTaxRate(new TaxFakeGetTaxRateRequest
            {
                Country = request.Country
            });

            response.Rate = getTaxRateResponse.Rate;

            return response;
        }
        public async Task<CalculateTaxForOrderResponse> CalculateTaxForOrder(CalculateTaxForOrderRequest request)
        {
            var response = new CalculateTaxForOrderResponse();

            var getTaxForOrderResponse = await _taxFakeClient.GetTaxForOrder(new TaxFakeGetTaxForOrderRequest
            {
                Amount = request.Amount,
                Country = request.Country
            });

            response.PreTaxAmount = getTaxForOrderResponse.PreTaxAmount;
            response.TaxRate = getTaxForOrderResponse.Rate;
            response.TaxAmount = getTaxForOrderResponse.TaxAmount;

            return response;
        }
    }
}
