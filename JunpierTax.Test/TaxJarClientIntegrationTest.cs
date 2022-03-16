using JuniperTax.TaxJar;
using JuniperTax.TaxJar.Models;
using JuniperTax.TaxJar.Request;
using JuniperTax.TaxJar.Response;
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
    public class TaxJarClientIntegrationTest
    {
        private WebApplication _app;
        private IServiceProvider _serviceProvider;

        private ITaxJarClient _taxJarClient;

        [SetUp]
        public void Setup()
        {
            _taxJarClient = _serviceProvider.GetRequiredService<ITaxJarClient>();
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

        static List<dynamic> Can_GetRatesForLocationTestCases = new List<dynamic>
        {
            new {
                Request = new RatesForLocationRequest { Zip = "63101-1201" },
                ExpectedResponse = new RatesForLocationResponse { Rate = new Rate { CombinedRate = 0.11679m } }
            },
            new {
                Request = new RatesForLocationRequest { Zip = "63101-1809" },
                ExpectedResponse = new RatesForLocationResponse { Rate = new Rate { CombinedRate = 0.09679m } }
            }
        };

        [Test]
        [TestCaseSource(nameof(Can_GetRatesForLocationTestCases))]
        public async Task Can_GetRatesForLocation(dynamic testCase)
        {
            var response = await _taxJarClient.RatesForLocation(testCase.Request);


            Assert.AreEqual(testCase.ExpectedResponse.Rate.CombinedRate, response?.Rate?.CombinedRate);

            Console.WriteLine(TestHelper.ToJson(response));
        }


        static List<dynamic> Can_GetTaxForOrderTestCases = new List<dynamic> {
            new {
                Request = new TaxForOrderRequest { 
                    Amount = 2.35m,
                    FromZip = "75002",
                    FromState = "TX",
                    ToZip = "75002",
                    ToState = "TX"
                },
                ExpectedResponse = new TaxForOrderResponse { 
                    Tax = new Tax { 
                        Rate = 0.0825m,
                        AmountToCollect = 0.19m
                    }
                }
            }
        };

        [Test]
        [TestCaseSource(nameof(Can_GetTaxForOrderTestCases))]
        public async Task Can_GetTaxForOrder(dynamic testCase)
        {
            var response = await _taxJarClient.TaxForOrder(testCase.Request);

            Assert.AreEqual(testCase.ExpectedResponse.Tax.Rate, response?.Tax?.Rate);
            Assert.AreEqual(testCase.ExpectedResponse.Tax.AmountToCollect, response?.Tax?.AmountToCollect);

            Console.WriteLine(TestHelper.ToJson(response));
        }
    }
}