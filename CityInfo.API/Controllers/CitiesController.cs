using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitiesController : ControllerBase
{
    private ICityInfoRepository _cityInfoRepository;

    public CitiesController(ICityInfoRepository cityInfoRepository)
    {
        _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
    }

    [HttpGet]
    public async Task<IActionResult> GetCities()
    {
        var cities = await _cityInfoRepository.GetCitiesAsync();
        return Ok(cities.Select(c => new CityWithoutPointsOfInterestDto()
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description
        }).ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetCity(int id)
    {
        // var cityDto = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == id);
        // if (cityDto == null)
        // {
        //     return NotFound();
        // }
        // return Ok(cityDto);
        return Ok();
    }
}
