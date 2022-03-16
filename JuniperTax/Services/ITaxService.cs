using JuniperTax.Contracts.Request;
using JuniperTax.Contracts.Response;

namespace JuniperTax
{
    public interface ITaxService
    {
        Task<GetTaxRatesByLocationResponse> GetTaxRatesByLocation(GetTaxRatesByLocationRequest request);
        Task<CalculateTaxForOrderResponse> CalculateTaxForOrder(CalculateTaxForOrderRequest request);
    }
}
