using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace JunpierTax.Test.Configuration
{
    public static class TestHarness
    {
        public static async Task<WebApplication> Startup(Action<IServiceCollection>? serviceMockRegistrations = null)
        {
            var apiKey = Environment.GetEnvironmentVariable("TaxJarApiKey", EnvironmentVariableTarget.Machine);
            Environment.SetEnvironmentVariable("TaxJarApiKey", apiKey);

            var registrations = serviceMockRegistrations ?? (collection => { });

            var app = JuniperTax.Api.Startup.BuildApp(null, registrations);

            await app.StartAsync();
            return app;
        }
    }
}
