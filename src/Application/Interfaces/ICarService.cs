namespace CarMauiApp.Application.Interfaces;

using CarMauiApp.Application.Models;

public interface ICarService
{
    Task<IReadOnlyList<CarModel>> GetCarsAsync();
}
