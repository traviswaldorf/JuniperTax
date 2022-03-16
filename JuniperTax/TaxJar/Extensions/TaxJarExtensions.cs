using JuniperTax.TaxJar.Response;

namespace JuniperTax.TaxJar.Extensions
{
    public static class TaxJarExtensions
    {
        public static bool IsSuccess(this TaxJarApiResponse response)
        {
            return (response.Status == null && response.Error == null);
        }
    }
}
