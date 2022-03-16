using JuniperTax.Contracts.Request;
using JuniperTax.Contracts.Response;
using JuniperTax.Services.Strategy.Factory;

namespace JuniperTax
{
    public class TaxService : ITaxService
    {
        private readonly ITaxStrategyFactory _taxStrategyFactory;

        public TaxService(ITaxStrategyFactory taxStrategyFactory)
        {
            _taxStrategyFactory = taxStrategyFactory;
        }

        public async Task<GetTaxRatesByLocationResponse> GetTaxRatesByLocation(GetTaxRatesByLocationRequest request)
        {
            var response = new GetTaxRatesByLocationResponse();
         
            var strategy = _taxStrategyFactory.GetTaxRatesByLocationStrategy(request);

            response = await strategy.GetTaxRatesByLocation(request);

            return response;
        }

        public async Task<CalculateTaxForOrderResponse> CalculateTaxForOrder(CalculateTaxForOrderRequest request)
        {
            var response = new CalculateTaxForOrderResponse();

            var strategy = _taxStrategyFactory.GetCalculateTaxForOrderStrategy(request);

            response = await strategy.CalculateTaxForOrder(request);

            return response;
        }
    }
}