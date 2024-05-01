using CityInfo.API.Entites;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.DbContexts;

public class CityInfoContext : DbContext
{
    public CityInfoContext(DbContextOptions<CityInfoContext> options)
        : base(options)
    {
    }

    public DbSet<City> Cities { get; set; }

    public DbSet<PointOfInterest> PointOfInterests { get; set; }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseSqlite("connectionstring");
    //     base.OnConfiguring(optionsBuilder);
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>().HasData(
            new City("New York City")
      
            {
                Id = 1,
                Description = "The one with that big park."
            },
            new City("Antwerp")
          
            {
                Id = 2,
                Description = "The one with the cathedral that was never really finished."
            },
            new City("Paris")
            {
                Id = 3,
                Description = "The one with that big tower."
            });

        modelBuilder.Entity<PointOfInterest>().HasData(
            new PointOfInterest("Central Park")
            {
                Id = 1,
                CityId = 1
            },
            new PointOfInterest("Empire State Building")
            {
                Id = 2,
                CityId = 1
            },
            new PointOfInterest("Cathedral of Our Lady")
            {
                Id = 3,
                CityId = 2
            },
            new PointOfInterest("Antwerp Central Station")
            {
                Id = 4,
                CityId = 2
            },
            new PointOfInterest("Eiffel Tower")
            {
                Id = 5,
                CityId = 3
            });
        
        
        
        base.OnModelCreating(modelBuilder);
    }
}
