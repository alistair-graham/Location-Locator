namespace Location_Locator.Services.LocationService
{
    public class LocationService : ILocationService
    {
        public LocationService(HttpClient httpClient, ILogger<LocationService> logger)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<Location> GetFromIPAddress(IPAddress ipAddress)
        {
        }

    }
}