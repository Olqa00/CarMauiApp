namespace CarMauiApp.Application.Interfaces;

using CarMauiApp.Application.Models;

public interface ICarRepository
{
    Task<IReadOnlyList<CarModel>> GetCarsAsync();
}
