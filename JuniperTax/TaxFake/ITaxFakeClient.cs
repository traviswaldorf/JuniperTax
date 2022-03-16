using JuniperTax.TaxFake.Request;
using JuniperTax.TaxFake.Response;

namespace JuniperTax.TaxMock
{
    public interface ITaxFakeClient
    {
        Task<TaxFakeGetTaxRateResponse> GetTaxRate(TaxFakeGetTaxRateRequest request);
        Task<TaxFakeGetTaxForOrderResponse> GetTaxForOrder(TaxFakeGetTaxForOrderRequest request);
    }
}
