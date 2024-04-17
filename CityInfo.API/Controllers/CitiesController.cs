using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitiesController : ControllerBase
{
    [HttpGet]
    public IActionResult GetCities()
    {
        return Ok(CitiesDataStore.Current.Cities);
    }

    [HttpGet("{id}")]
    public IActionResult GetCity(int id)
    {
        var cityDto = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);
        if (cityDto == null)
        {
            return NotFound();
        }
        return Ok(cityDto);
    }
}
