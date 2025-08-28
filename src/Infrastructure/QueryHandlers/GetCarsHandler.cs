namespace CarMauiApp.Infrastructure.QueryHandlers;

using CarMauiApp.Application.Interfaces;
using CarMauiApp.Application.Models;
using CarMauiApp.Application.Queries;

internal sealed class GetCarsHandler : IRequestHandler<GetCars, IReadOnlyList<CarModel>>
{
    private readonly ICarRepository carService;
    private readonly ILogger<GetCarsHandler> logger;

    public GetCarsHandler(ICarRepository carService, ILogger<GetCarsHandler> logger)
    {
        this.carService = carService;
        this.logger = logger;
    }

    public async Task<IReadOnlyList<CarModel>> Handle(GetCars request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Try to get cars.");

        var cars = await this.carService.GetCarsAsync();

        return cars;
    }
}
