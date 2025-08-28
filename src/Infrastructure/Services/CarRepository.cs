namespace CarMauiApp.Infrastructure.Services;

using CarMauiApp.Application.Interfaces;
using CarMauiApp.Application.Models;
using CarMauiApp.Domain.Entities;
using CarMauiApp.Infrastructure.SqlServer;

internal sealed class CarRepository : ICarRepository
{
    private const string CAR_ID = "carId";
    private readonly DbSet<CarEntity> cars;
    private readonly CarMauiDbContext dbContext;
    private readonly ILogger<CarRepository> logger;

    public CarRepository(CarMauiDbContext dbContext, ILogger<CarRepository> logger)
    {
        this.cars = dbContext.Cars;
        this.dbContext = dbContext;
        this.logger = logger;
    }

    public async Task AddCarAsync(CarEntity entity)
    {
        using var loggerScope = this.logger.BeginScope(
            (CAR_ID, entity.Id)
        );

        this.logger.LogInformation("Try to add car to db");

        await this.cars.AddAsync(entity);
        await this.dbContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<CarModel>> GetCarsAsync()
    {
        this.logger.LogInformation("Try to get cars from db");

        var carEntities = await this.cars.ToListAsync();

        var carModels = carEntities
            .Select(carEntity => new CarModel
            {
                Id = carEntity.Id,
                Make = carEntity.Make,
                Model = carEntity.Model,
                Vin = carEntity.Vin,
            })
            .ToList();

        return carModels;
    }
}
