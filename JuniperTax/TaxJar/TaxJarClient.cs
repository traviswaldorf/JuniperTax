using JuniperTax.TaxJar.Request;
using JuniperTax.TaxJar.Response;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace JuniperTax.TaxJar
{
    public class TaxJarClient : ITaxJarClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        private const string ApiVersion = "2022-01-24";

        public TaxJarClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        // Set environment variable for TaxJarApiKey
        // This could otherwise be managed with Secrets Manager https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows
        // Or injected from Azure Key Vault as part of the build
        public string GetApiKey()
        {
            return _configuration["TaxJarApiKey"];
        }

        //POST https://api.taxjar.com/v2/taxes
        public async Task<TaxForOrderResponse> TaxForOrder(TaxForOrderRequest request)
        {
            var httpClient = GetHttpClient();
            var httpContent = new StringContent(SerializeHttpRequest(request), Encoding.UTF8, Application.Json);
            var httpResponseMessage = await httpClient.PostAsync($"taxes", httpContent);

            var response = await DeserializeHttpResponse<TaxForOrderResponse>(httpResponseMessage);

            return response;
        }

        //GET https://api.taxjar.com/v2/rates/:zip
        public async Task<RatesForLocationResponse> RatesForLocation(RatesForLocationRequest request)
        {
            var httpClient = GetHttpClient();
            var httpResponseMessage = await httpClient.GetAsync($"rates/{request.Zip}");

            var response = await DeserializeHttpResponse<RatesForLocationResponse>(httpResponseMessage);

            return response;
        }

        public string SerializeHttpRequest<T>(T request)
        {
            var serializedRequest = JsonSerializer.Serialize<T>(request, new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });

            return serializedRequest;
        }

        public async Task<T> DeserializeHttpResponse<T>(HttpResponseMessage httpResponseMessage)
        {
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var jsonSerializerOptions = new JsonSerializerOptions
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };

            var response = await JsonSerializer.DeserializeAsync<T>(contentStream, jsonSerializerOptions);

            if (response == null)
            {
                response = default;
            }

            return response;
        }

        private HttpClient GetHttpClient()
        {
            var httpClient = _httpClientFactory.CreateClient(TaxJarConstants.HttpClientName);
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GetApiKey());
            httpClient.DefaultRequestHeaders.Add("x-api-version", ApiVersion); // We're maintaining a client here against a version controlled api, so we should control versioning so production doesn't break overnight

            return httpClient;
        }
    }
}
