using System.Net;
using Location_Locator.Models;
using Location_Locator.Services.LocationService;
using Microsoft.AspNetCore.Mvc;

namespace LocationAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationController : ControllerBase
{
    private readonly ILogger<LocationController> _logger;
    private readonly ILocationService _locationService;

    public LocationController(ILogger<LocationController> logger, ILocationService locationService)
    {
        _logger = logger;
        _locationService = locationService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Location>> Get()
    {
        var requestIPAddress = HttpContext.Connection.RemoteIpAddress;


        if (requestIPAddress == null || string.IsNullOrEmpty(requestIPAddress.ToString()))
        {
            return BadRequest("Failed to extract IP Address from the request.");
        }

        var location = await _locationService.GetFromIPAddress(requestIPAddress);

        if (location.Errors.Any())
        {
            return BadRequest(location.Errors);
        }

        return Ok(location);
    }
}

