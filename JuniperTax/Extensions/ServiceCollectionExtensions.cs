using JuniperTax.Services.Strategy.Factory;
using JuniperTax.Services.Strategy.Strategies;
using JuniperTax.TaxJar;
using JuniperTax.TaxMock;
using Microsoft.Extensions.DependencyInjection;

namespace JuniperTax.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddJuniperTax(this IServiceCollection services)
        {
            services.AddSingleton<ITaxService, TaxService>();
            services.AddTaxJar();
            services.AddTaxFake();

            services.AddSingleton<TaxJarTaxStrategy>();
            services.AddSingleton<TaxFakeTaxStrategy>();
            services.AddSingleton<ITaxStrategyFactory, TaxStrategyFactory>();
        }

        public static void AddTaxJar(this IServiceCollection services)
        {
            services.AddSingleton<ITaxJarClient, TaxJarClient>();
            services.AddHttpClient(TaxJarConstants.HttpClientName, httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://api.taxjar.com/v2/");
            });
        }

        public static void AddTaxFake(this IServiceCollection services)
        {
            services.AddSingleton<ITaxFakeClient, TaxFakeClient>();
        }
    }
}
