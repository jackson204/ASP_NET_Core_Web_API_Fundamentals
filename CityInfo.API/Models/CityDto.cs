namespace CityInfo.API.Models;

public class CityDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
    
    public int NumberOfPointsOfInterest => PointOfInterestDtos.Count;

    public ICollection<PointOfInterestDto> PointOfInterestDtos { get; set; } = new List<PointOfInterestDto>();
}
