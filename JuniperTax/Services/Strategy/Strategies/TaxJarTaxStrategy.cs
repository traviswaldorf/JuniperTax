using JuniperTax.Contracts.Extensions;
using JuniperTax.Contracts.Request;
using JuniperTax.Contracts.Response;
using JuniperTax.TaxJar;
using JuniperTax.TaxJar.Request;

namespace JuniperTax.Services.Strategy.Strategies
{
    public class TaxJarTaxStrategy : ITaxStrategy
    {
        private readonly ITaxJarClient _taxJarClient;

        public TaxJarTaxStrategy(ITaxJarClient taxJarClient)
        {
            _taxJarClient = taxJarClient;
        }

        public async Task<GetTaxRatesByLocationResponse> GetTaxRatesByLocation(GetTaxRatesByLocationRequest request)
        {
            var response = new GetTaxRatesByLocationResponse();

            var ratesForLocationResponse = await _taxJarClient.RatesForLocation(new RatesForLocationRequest
            {
                Zip = request.Zip
            });
            if (ratesForLocationResponse.Status != null || ratesForLocationResponse.Error != null)
            {
                var errorMessage = "Error getting RatesForLocation from TaxJar.";
                if (ratesForLocationResponse?.Error != null)
                {
                    errorMessage += $" {ratesForLocationResponse?.Error}.";
                }
                if (ratesForLocationResponse?.Detail != null)
                {
                    errorMessage += $" {ratesForLocationResponse?.Detail}.";
                }
                return response.AddError(errorMessage);
            }

            if (ratesForLocationResponse?.Rate?.CombinedRate == null)
            {
                return response.AddError("Error getting RatesForLocation from TaxJar. Rate is null.");
            }

            response.Rate = ratesForLocationResponse.Rate.CombinedRate;

            return response;
        }

        public async Task<CalculateTaxForOrderResponse> CalculateTaxForOrder(CalculateTaxForOrderRequest request)
        {
            var response = new CalculateTaxForOrderResponse();

            var taxForOrderResponse = await _taxJarClient.TaxForOrder(new TaxForOrderRequest
            {
                Amount = request.Amount,
                ToZip = request.Zip,
                ToState = request.State,
                FromZip = request.Zip,
                FromState = request.State
            });
            if (taxForOrderResponse.Status != null || taxForOrderResponse.Error != null)
            {
                var errorMessage = "Error getting RatesForLocation from TaxJar.";
                if (taxForOrderResponse?.Error != null)
                {
                    errorMessage += $" {taxForOrderResponse?.Error}.";
                }
                if (taxForOrderResponse?.Detail != null)
                {
                    errorMessage += $" {taxForOrderResponse?.Detail}.";
                }
                return response.AddError(errorMessage);
            }

            if (taxForOrderResponse?.Tax == null)
            {
                return response.AddError("Error getting TaxForOrder from TaxJar. Tax is null.");
            }

            response.PreTaxAmount = taxForOrderResponse.Tax.OrderTotalAmount;
            response.TaxRate = taxForOrderResponse.Tax.Rate;
            response.TaxAmount = taxForOrderResponse.Tax.AmountToCollect;

            return response;
        }
    }
}
