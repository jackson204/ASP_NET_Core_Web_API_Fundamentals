using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitiesController : ControllerBase
{
    private readonly  CitiesDataStore _citiesDataStore;
    public CitiesController(CitiesDataStore citiesDataStore)
    {
        _citiesDataStore = citiesDataStore;
    }
    [HttpGet]
    public IActionResult GetCities()
    {
        return Ok(_citiesDataStore.Cities);
    }

    [HttpGet("{id}")]
    public IActionResult GetCity(int id)
    {
        var cityDto = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == id);
        if (cityDto == null)
        {
            return NotFound();
        }
        return Ok(cityDto);
    }
}
