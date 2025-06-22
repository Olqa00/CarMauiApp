namespace CarMauiApp.Infrastructure.QueryHandlers;

using CarMauiApp.Application.Interfaces;
using CarMauiApp.Application.Models;
using CarMauiApp.Application.Queries;

internal sealed class GetCarsHandler : IRequestHandler<GetCars, IReadOnlyList<CarModel>>
{
    private readonly ICarService carService;

    public GetCarsHandler(ICarService carService)
    {
        this.carService = carService;
    }

    public async Task<IReadOnlyList<CarModel>> Handle(GetCars request, CancellationToken cancellationToken)
    {
        var cars = await this.carService.GetCarsAsync();

        return cars;
    }
}
