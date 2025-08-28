namespace CarMauiApp.Application.Interfaces;

using CarMauiApp.Application.Models;
using CarMauiApp.Domain.Entities;

public interface ICarRepository
{
    Task AddCarAsync(CarEntity entity);
    Task<IReadOnlyList<CarModel>> GetCarsAsync();
}
