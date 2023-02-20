using System;
using Moq;
using Location_Locator.Services.LocationService;
using Microsoft.Extensions.Logging;
using System.Net;

namespace LocationAPI.Tests.Tests.Integration.Services
{
    public class LocationServiceTests
    {
        private readonly ILocationService _locationService;
        private const string BASE_URI = "http://ip-api.com/json/";
        private const string PRIVATE_IP_ADDRESS = "172.16.0.0";
        private const string RESERVED_IP_ADDRESS = "127.0.0.1";

        public LocationServiceTests()
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri(BASE_URI)
            };
            var fakeLogger = Mock.Of<ILogger<LocationService>>();

            _locationService = new LocationService(client, fakeLogger);
        }

        [Fact]
        public async void GetFromIpAddress_WithValidIP_ReturnsSuccess()
        {
            var validIPAddress = IPAddress.Parse("24.48.0.1");

            var response = await _locationService.GetFromIPAddress(validIPAddress);

            Assert.NotNull(response);
            Assert.Equal(validIPAddress.ToString(), response.IPAddress);
            Assert.False(string.IsNullOrEmpty(response.Country));
            Assert.False(string.IsNullOrEmpty(response.RegionName));
            Assert.False(string.IsNullOrEmpty(response.City));
            Assert.Empty(response.Errors);
        }

    }
}

