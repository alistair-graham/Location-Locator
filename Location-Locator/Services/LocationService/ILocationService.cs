using System;
using System.Net;
using Location_Locator.Models;

namespace Location_Locator.Services.LocationService
{
	public interface ILocationService
	{
		public Task<Location> GetFromIPAddress(IPAddress ipAddress);
	}
}

