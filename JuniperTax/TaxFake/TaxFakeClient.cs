using JuniperTax.TaxFake.Request;
using JuniperTax.TaxFake.Response;

namespace JuniperTax.TaxMock
{
    public class TaxFakeClient : ITaxFakeClient
    {
        public TaxFakeClient()
        {
        }

        public async Task<TaxFakeGetTaxRateResponse> GetTaxRate(TaxFakeGetTaxRateRequest request)
        {
            var response = new TaxFakeGetTaxRateResponse();

            if (request.Country != "CA")
            {
                return response;
            }

            response.Rate = 0.13m;
            return response;
        }

        public async Task<TaxFakeGetTaxForOrderResponse> GetTaxForOrder(TaxFakeGetTaxForOrderRequest request)
        {
            var response = new TaxFakeGetTaxForOrderResponse();

            if (request.Country != "CA")
            {
                return response;
            }

            var getTaxRateResponse = await GetTaxRate(new TaxFakeGetTaxRateRequest { 
                Country = request.Country
            });

            response.PreTaxAmount = request.Amount;
            response.Rate = getTaxRateResponse.Rate;
            response.TaxAmount = getTaxRateResponse.Rate * request.Amount;

            return response;
        }
    }
}
