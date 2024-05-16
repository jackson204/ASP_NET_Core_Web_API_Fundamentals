using CityInfo.API.Entites;
using CityInfo.API.Entities;

namespace CityInfo.API.Services;

public interface ICityInfoRepository
{
    Task<IEnumerable<City>> GetCitiesAsync();
    Task<IEnumerable<City>> GetCitiesAsync(string? name);
    Task<City?> GetCityAsync(int cityId , bool includePointsOfInterest);

    Task<IEnumerable<PointOfInterest>> GetPointOfInterestsForCityAsync(int cityId );
    
    Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId);
}
