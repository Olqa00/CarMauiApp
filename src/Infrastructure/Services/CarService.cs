namespace CarMauiApp.Infrastructure.Services;

using CarMauiApp.Application.Interfaces;
using CarMauiApp.Application.Models;

internal sealed class CarService : ICarService
{
    public List<CarModel> GetCars()
    {
        var car1 = new CarModel
        {
            Id = 1,
            Make = "Toyota",
            Model = "Corolla",
            Vin = "1HGBH41JXMN109186",
        };

        var car2 = new CarModel
        {
            Id = 2,
            Make = "Honda",
            Model = "Civic",
            Vin = "1HGBH41JXMN109187",
        };

        var car3 = new CarModel
        {
            Id = 3,
            Make = "Ford",
            Model = "Focus",
            Vin = "1HGBH41JXMN109188",
        };

        var cars = new List<CarModel>
        {
            car1, car2, car3,
        };

        return cars;
    }
}
