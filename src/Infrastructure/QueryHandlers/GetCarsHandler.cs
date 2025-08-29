namespace CarMauiApp.Infrastructure.QueryHandlers;

using CarMauiApp.Application.Interfaces;
using CarMauiApp.Application.Models;
using CarMauiApp.Application.Queries;

internal sealed class GetCarsHandler : IRequestHandler<GetCars, IReadOnlyList<CarModel>>
{
    private readonly ILogger<GetCarsHandler> logger;
    private readonly ICarRepository repository;

    public GetCarsHandler(ILogger<GetCarsHandler> logger, ICarRepository repository)
    {
        this.logger = logger;
        this.repository = repository;
    }

    public async Task<IReadOnlyList<CarModel>> Handle(GetCars request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Try to get cars.");

        var cars = await this.repository.GetCarsAsync();

        return cars;
    }
}
