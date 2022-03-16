using JuniperTax;
using JuniperTax.Contracts.Request;
using JuniperTax.Contracts.Response;
using JunpierTax.Test.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JunpierTax.Test
{
    [TestFixture]
    public class TaxServiceIntegrationTest
    {
        private WebApplication _app;
        private IServiceProvider _serviceProvider;

        private ITaxService _taxService;

        [SetUp]
        public void Setup()
        {
            _taxService = _serviceProvider.GetRequiredService<ITaxService>();
        }

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            _app = await TestHarness.Startup();
            _serviceProvider = _app.Services;
        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
            await _app.StopAsync(CancellationToken.None);
            await _app.DisposeAsync();
        }

        static List<dynamic> Can_GetTaxRatesByLocationTestCases = new List<dynamic>
        {
            new {
                Request = new GetTaxRatesByLocationRequest { Zip = "63101-1201" },
                ExpectedResponse = new GetTaxRatesByLocationResponse { Rate = 0.11679m }
            },
            new {
                Request = new GetTaxRatesByLocationRequest { Zip = "63101-1809" },
                ExpectedResponse = new GetTaxRatesByLocationResponse { Rate = 0.09679m }
            },
            new {
                Request = new GetTaxRatesByLocationRequest { Country = "CA" },
                ExpectedResponse = new GetTaxRatesByLocationResponse { Rate = 0.13m }
            }
        };

        [Test]
        [TestCaseSource(nameof(Can_GetTaxRatesByLocationTestCases))]
        public async Task Can_GetTaxRatesByLocation(dynamic testCase)
        {
            var response = await _taxService.GetTaxRatesByLocation(testCase.Request);

            Assert.AreEqual(testCase.ExpectedResponse.Rate, response?.Rate);

            Console.WriteLine(TestHelper.ToJson(response));
        }


        static List<dynamic> Can_GetTaxForOrderTestCases = new List<dynamic> {
            new {
                Request = new CalculateTaxForOrderRequest { 
                    Amount = 2.35m,
                    Zip = "75002",
                    State = "TX"
                },
                ExpectedResponse = new CalculateTaxForOrderResponse { 
                    PreTaxAmount = 2.35m,
                    TaxRate = 0.0825m,
                    TaxAmount = 0.19m
                }
            },
            new {
                Request = new CalculateTaxForOrderRequest {
                    Amount = 2.35m,
                    Country = "CA",
                },
                ExpectedResponse = new CalculateTaxForOrderResponse {
                    PreTaxAmount = 2.35m,
                    TaxRate = 0.13m,
                    TaxAmount = 0.3055m
                }
            }
        };

        [Test]
        [TestCaseSource(nameof(Can_GetTaxForOrderTestCases))]
        public async Task Can_GetTaxForOrder(dynamic testCase)
        {
            var response = await _taxService.CalculateTaxForOrder(testCase.Request);

            Assert.AreEqual(testCase.ExpectedResponse.PreTaxAmount, response?.PreTaxAmount);
            Assert.AreEqual(testCase.ExpectedResponse.TaxRate, response?.TaxRate);
            Assert.AreEqual(testCase.ExpectedResponse.TaxAmount, response?.TaxAmount);

            Console.WriteLine(TestHelper.ToJson(response));
        }
    }
}