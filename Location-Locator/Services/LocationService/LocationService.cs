using System;
using System.Net;
using Location_Locator.Models;
using LocationAPI.Controllers;

namespace Location_Locator.Services.LocationService
{
    public class LocationService : ILocationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<LocationService> _logger;
        private readonly string STATUS_SUCCESS = "success";

        public LocationService(HttpClient httpClient, ILogger<LocationService> logger)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<Location> GetFromIPAddress(IPAddress ipAddress)
        {
            var response = await _httpClient.GetFromJsonAsync<LocationResponse>($"{ipAddress.ToString()}?fields=49177");

            if (response?.Status == STATUS_SUCCESS)
            {
                return Map(response, ipAddress);
            }

        private Location Map(LocationResponse response, IPAddress ipAddress)
        {
            return new Location
            {
                IPAddress = ipAddress.ToString(),
                Country = response.Country,
                RegionName = response.RegionName,
                City = response.City,
            };
        }

        private class LocationResponse
        {
            public string? Status { get; set; }
            public string? Message { get; set; }
            public string? Country { get; set; }
            public string? RegionName { get; set; }
            public string? City { get; set; }
        }
    }
}