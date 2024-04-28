using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers;

[ApiController]
[Route("api/cities/{cityId}/pointsofinterest")]
public class PointsOfInterestController : ControllerBase
{
    private readonly ILogger<PointsOfInterestController> _logger;
    private readonly ILocalMailService _mailService;

    public PointsOfInterestController(ILogger<PointsOfInterestController> logger, ILocalMailService mailService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mailService = mailService;
    }

    //參考文件 https://www.cnblogs.com/zhaoshujie/p/12306481.html
    [HttpPost]
    public IActionResult CreatePointOfInterest(int cityId, [FromBody] PointOfInterestForCreationDto pointOfInterest)
    {
        if (pointOfInterest == null)
        {
            return BadRequest();
        }
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
        if (city == null)
        {
            return NotFound();
        }
        var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(c => c.PointOfInterestDtos).Max(p => p.Id);
        var finalPointOfInterest = new PointOfInterestDto
        {
            Id = ++maxPointOfInterestId,
            Name = pointOfInterest.Name,
            Description = pointOfInterest.Description
        };
        city.PointOfInterestDtos.Add(finalPointOfInterest);

        return CreatedAtRoute("GetPointOfInterest",
            new
            {
                cityId,
                pointsofinterestid = finalPointOfInterest.Id
            },
            finalPointOfInterest);
    }

    [HttpDelete("{pointsofinterestid}")]
    public IActionResult DeletePointOfInterest(int cityId, int pointsofinterestid)
    {
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
        if (city == null)
        {
            return NotFound();
        }
        var pointOfInterestFromStore = city.PointOfInterestDtos.FirstOrDefault(p => p.Id == pointsofinterestid);
        if (pointOfInterestFromStore == null)
        {
            return NotFound();
        }
        city.PointOfInterestDtos.Remove(pointOfInterestFromStore);

        _mailService.Send("Point of interest deleted.",
            $"Point of interest {pointOfInterestFromStore.Name} with id {pointOfInterestFromStore.Id} was deleted.");
        return NoContent();
    }

    [HttpGet("{pointsofinterestid}", Name = "GetPointOfInterest")]
    public IActionResult GetPointOfInterest(int cityId, int pointsofinterestid)
    {
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
        if (city == null)
        {
            return NotFound();
        }
        var pointOfInterest = city.PointOfInterestDtos.FirstOrDefault(p => p.Id == pointsofinterestid);
        if (pointOfInterest == null)
        {
            return NotFound();
        }
        return Ok(pointOfInterest);
    }

    [HttpGet]
    public IActionResult GetPointsOfInterest(int cityId)
    {
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
        if (city == null)
        {
            _logger.LogInformation("City with id {cityId} wasn't found.", cityId);
            return NotFound();
        }
        return Ok(city.PointOfInterestDtos);
    }

    [HttpPut("{pointsofinterestid}")]
    public IActionResult UpdatePointOfInterest(int cityId, int pointsofinterestid, PointOfInterestForUpdateDto pointOfInterest)
    {
        if (pointOfInterest == null)
        {
            return BadRequest();
        }
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
        if (city == null)
        {
            return NotFound();
        }
        var pointOfInterestFromStore = city.PointOfInterestDtos.FirstOrDefault(p => p.Id == pointsofinterestid);
        if (pointOfInterestFromStore == null)
        {
            return NotFound();
        }
        pointOfInterestFromStore.Name = pointOfInterest.Name;
        pointOfInterestFromStore.Description = pointOfInterest.Description;

        return NoContent();
    }
}
