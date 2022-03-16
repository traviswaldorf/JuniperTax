using JuniperTax.Contracts.Request;
using JuniperTax.Contracts.Response;

namespace JuniperTax.Services.Strategy.Strategies
{
    public interface ITaxStrategy
    {
        Task<GetTaxRatesByLocationResponse> GetTaxRatesByLocation(GetTaxRatesByLocationRequest request);
        Task<CalculateTaxForOrderResponse> CalculateTaxForOrder(CalculateTaxForOrderRequest request);
    }
}
