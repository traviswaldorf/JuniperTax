using JuniperTax.Contracts.Request;
using JuniperTax.Services.Strategy.Strategies;

namespace JuniperTax.Services.Strategy.Factory
{
    public class TaxStrategyFactory : ITaxStrategyFactory
    {
        private readonly TaxJarTaxStrategy _taxJarTaxStrategy;
        private readonly TaxFakeTaxStrategy _taxFakeTaxStrategy;

        public TaxStrategyFactory(TaxJarTaxStrategy taxJarTaxStrategy,
            TaxFakeTaxStrategy taxFakeTaxStrategy)
        {
            _taxJarTaxStrategy = taxJarTaxStrategy;
            _taxFakeTaxStrategy = taxFakeTaxStrategy;
        }

        public ITaxStrategy GetTaxRatesByLocationStrategy(GetTaxRatesByLocationRequest request)
        {
            if (request.Country == "CA")
            {
                return _taxFakeTaxStrategy;
            }

            return _taxJarTaxStrategy;
        }

        public ITaxStrategy GetCalculateTaxForOrderStrategy(CalculateTaxForOrderRequest request)
        {
            if (request.Country == "CA")
            {
                return _taxFakeTaxStrategy;
            }

            return _taxJarTaxStrategy;
        }
    }
}
