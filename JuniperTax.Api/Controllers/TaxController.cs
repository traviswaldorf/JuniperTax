using JuniperTax.Contracts.Model;
using JuniperTax.Contracts.Request;
using JuniperTax.Contracts.Response;
using Microsoft.AspNetCore.Mvc;

namespace JuniperTax.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TaxController : ControllerBase
    {
        private readonly ILogger<TaxController> _logger;
        private readonly ITaxService _taxService;

        public TaxController(ILogger<TaxController> logger,
            ITaxService taxService)
        {
            _logger = logger;
            _taxService = taxService;
        }

        [HttpPost(Name = "GetTaxRatesByLocation")]
        public async Task<GetTaxRatesByLocationResponse> GetTaxRatesByLocation([FromBody]GetTaxRatesByLocationRequest request)
        {
            try
            {
                var response = await _taxService.GetTaxRatesByLocation(request);
                return response;
            }
            catch (Exception e)
            {
                return await HandleException<GetTaxRatesByLocationResponse>(e);
            }
        }

        [HttpPost(Name = "CalculateTaxForOrder")]
        public async Task<CalculateTaxForOrderResponse> CalculateTaxForOrder([FromBody] CalculateTaxForOrderRequest request)
        {
            try
            {
                var response = await _taxService.CalculateTaxForOrder(request);
                return response;
            }
            catch (Exception e)
            {
                return await HandleException<CalculateTaxForOrderResponse>(e);
            }
        }

        private async Task<T> HandleException<T>(Exception e) where T : ApiResponse, new()
        {
            return new T
            {
                Error = new Error
                {
                    Message = $"An exception has occurred. {e.Message}"
                }
            };
        }
    }
}