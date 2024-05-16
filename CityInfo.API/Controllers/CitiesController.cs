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

    public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper)
    {
        _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    //http://localhost:5045/api/Cities?name=Antwerp
    [HttpGet]
    public async Task<IActionResult> GetCities( string? name)
    {
        var cities = await _cityInfoRepository.GetCitiesAsync(name);
        return Ok(_mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cities));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCity(int id, bool includePointsOfInterest = false)
    {
        var city = await _cityInfoRepository.GetCityAsync(id, includePointsOfInterest);
        if (city == null)
        {
            return NotFound();
        }
        if (includePointsOfInterest)
        {
            return Ok(_mapper.Map<CityDto>(city));
        }
        return Ok(_mapper.Map<CityWithoutPointsOfInterestDto>(city));
    }
}
