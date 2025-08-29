namespace CarMauiApp.Application.Interfaces;

using CarMauiApp.Application.Models;
using CarMauiApp.Domain.Entities;

public interface ICarRepository
{
    Task AddCarAsync(CarEntity entity);
    Task DeleteCarAsync(Guid id);
    Task<CarModel?> GetCarByIdAsync(Guid id);
    Task<IReadOnlyList<CarModel>> GetCarsAsync();
}
