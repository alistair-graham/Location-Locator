using System;
using System.Text.Json.Serialization;

namespace Location_Locator.Models
{
	public class Location
	{
        public string? IPAddress { get; set; }
        public string? Country { get; set; }
        public string? RegionName { get; set; }
        public string? City { get; set; }

    }
}

