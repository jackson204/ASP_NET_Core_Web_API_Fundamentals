using CityInfo.API.Entites;

namespace CityInfo.API.Services;

public interface ICityInfoRepository
{
    Task<IEnumerable<City>> GetCitiesAsync();

    Task<City?> GetCityAsync(int cityId , bool includePointsOfInterest);

    Task<IEnumerable<PointOfInterest>> GetPointOfInterestsForCityAsync(int cityId );
    
    Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId);
}
