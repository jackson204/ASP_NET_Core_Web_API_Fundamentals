using CityInfo.API.DbContexts;
using CityInfo.API.Entites;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Services;

public class CityInfoRepository : ICityInfoRepository
{
    private CityInfoContext _context;

    public CityInfoRepository(CityInfoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<City>> GetCitiesAsync()
    {
        return await _context.Cities.OrderBy(r => r.Name).ToListAsync();
    }

    public Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest)
    {
        if (includePointsOfInterest)
        {
            return _context.Cities.Include(c => c.PointOfInterestDtos).FirstOrDefaultAsync(c => c.Id == cityId);
        }

        return _context.Cities.FirstOrDefaultAsync(c => c.Id == cityId);
    }

    public async Task<IEnumerable<PointOfInterest>> GetPointOfInterestsForCityAsync(int cityId)
    {
        return await _context.PointOfInterests.Where(p => p.CityId == cityId).ToListAsync();
    }

    public async Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId)
    {
        return await _context.PointOfInterests.Where(p => p.CityId == cityId && p.Id == pointOfInterestId).FirstOrDefaultAsync();
    }
}
