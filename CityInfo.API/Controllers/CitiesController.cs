using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitiesController : ControllerBase
{
    private ICityInfoRepository _cityInfoRepository;
    private readonly IMapper _mapper;

    public CitiesController(ICityInfoRepository cityInfoRepository,IMapper mapper)
    {
        _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
        _mapper = mapper?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<IActionResult> GetCities()
    {
        var cities = await _cityInfoRepository.GetCitiesAsync();
        return Ok(_mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cities));
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
