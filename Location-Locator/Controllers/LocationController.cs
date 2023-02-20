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
    }
}

