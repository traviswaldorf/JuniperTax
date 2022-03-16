using JuniperTax.Contracts.Model;
using JuniperTax.Contracts.Response;

namespace JuniperTax.Contracts.Extensions
{
    public static class ErrorExtensions
    {
        public static T AddError<T>(this T response, string message) where T : ApiResponse
        {
            response.Error = new Error
            {
                Message = message
            };
            return response;
        }
    }
}
