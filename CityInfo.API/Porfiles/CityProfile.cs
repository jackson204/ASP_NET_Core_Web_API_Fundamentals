using AutoMapper;
using CityInfo.API.Entities;

namespace CityInfo.API.Porfiles;

public class CityProfile : Profile
{
    public CityProfile()
    {
        CreateMap<City, Models.CityWithoutPointsOfInterestDto>();
        
        CreateMap<City, Models.CityDto>();
    }
}
