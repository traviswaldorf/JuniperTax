using JuniperTax.Contracts.Request;
using JuniperTax.Services.Strategy.Strategies;

namespace JuniperTax.Services.Strategy.Factory
{
    public interface ITaxStrategyFactory
    {
        ITaxStrategy GetTaxRatesByLocationStrategy(GetTaxRatesByLocationRequest request);
        ITaxStrategy GetCalculateTaxForOrderStrategy(CalculateTaxForOrderRequest request);
    }
}
