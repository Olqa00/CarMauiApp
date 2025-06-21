namespace CarMauiApp.Application.Interfaces;

using CarMauiApp.Application.Models;

public interface ICarService
{
    List<CarModel> GetCars();
}
