using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CityInfo.API.Entites;

namespace CityInfo.API.Entities;

public class City
{
    [Key]
    [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [MaxLength(200)]
    public string? Description { get; set; }

    public ICollection<PointOfInterest> PointOfInterestDtos { get; set; } = new List<PointOfInterest>();

    public City(string name)
    {
        Name = name;
    }
}