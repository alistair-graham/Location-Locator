using Location_Locator.Models;
using Location_Locator.Services.LocationService;
using LocationAPI.Tests.Tests.Integration.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics.Metrics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using Xunit;
using Xunit.Sdk;

namespace LocationAPI.Tests.Tests.Integration.Controllers
{
    public class LocationControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {

        private readonly WebApplicationFactory<Program> _factory;
        private const string PRIVATE_IP_ADDRESS = "172.16.0.0";
        private const string RESERVED_IP_ADDRESS = "127.0.0.1";

        public LocationControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async void Get_WithValidIPAddress_ReturnsSuccessResponse()
        {
            var factoryWithFakes = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.UseSetting("https_port", "5081");
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton<IStartupFilter>(new CustomRemoteIpStartupFilter(IPAddress.Parse("24.48.0.1")));
                    services.AddHttpClient<ILocationService, StubLocationService>();
                });
            });

            var client = factoryWithFakes.CreateClient();
            var response = await client.GetAsync("api/location");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

            var locationResponse = await response.Content.ReadFromJsonAsync<Location>();

            Assert.NotNull(locationResponse);
            Assert.Equal("24.48.0.1", locationResponse.IPAddress);
            Assert.Equal("United Kingdom", locationResponse.Country);
            Assert.Equal("England", locationResponse.RegionName);
            Assert.Equal("London", locationResponse.City);
        }


        [Fact]
        public async void Get_WithoutIPAddress_ReturnsBadResponse()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/location");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var responseMessage = await response.Content.ReadAsStringAsync();
            Assert.Equal("Failed to extract IP Address from the request.", responseMessage);
        }
    }
}
