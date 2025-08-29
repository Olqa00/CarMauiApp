namespace CarMauiApp.Infrastructure.QueryHandlers;

using CarMauiApp.Application.Interfaces;
using CarMauiApp.Application.Models;
using CarMauiApp.Application.Queries;
using CarMauiApp.Infrastructure.Exceptions;

internal sealed class GetCarByIdHandler : IRequestHandler<GetCarById, CarModel>
{
    private const string CAR_ID = "carId";
    private readonly ILogger<GetCarByIdHandler> logger;
    private readonly ICarRepository repository;

    public GetCarByIdHandler(ILogger<GetCarByIdHandler> logger, ICarRepository repository)
    {
        this.logger = logger;
        this.repository = repository;
    }

    public async Task<CarModel> Handle(GetCarById request, CancellationToken cancellationToken)
    {
        using var loggerScope = this.logger.BeginScope(
            (CAR_ID, request.Id)
        );

        this.logger.LogInformation("Try to get car.");

        var car = await this.repository.GetCarByIdAsync(request.Id);

        if (car is null)
        {
            throw new CarNotFoundException(request.Id);
        }

        return car;
    }
}
