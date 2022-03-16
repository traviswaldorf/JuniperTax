using JuniperTax.TaxJar.Request;
using JuniperTax.TaxJar.Response;

namespace JuniperTax.TaxJar
{
    public interface ITaxJarClient
    {
        Task<TaxForOrderResponse> TaxForOrder(TaxForOrderRequest request);
        Task<RatesForLocationResponse> RatesForLocation(RatesForLocationRequest request);
    }
}
