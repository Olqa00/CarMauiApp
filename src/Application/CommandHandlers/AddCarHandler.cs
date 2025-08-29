namespace CarMauiApp.Application.CommandHandlers;

using CarMauiApp.Application.Commands;
using CarMauiApp.Application.Exceptions;
using CarMauiApp.Application.Interfaces;
using CarMauiApp.Domain.Entities;

internal sealed class AddCarHandler : IRequestHandler<AddCar>
{
    private const string CAR_ID = "carId";
    private readonly ILogger<AddCarHandler> logger;
    private readonly ICarRepository repository;

    public AddCarHandler(ICarRepository repository, ILogger<AddCarHandler> logger)
    {
        this.repository = repository;
        this.logger = logger;
    }

    public async Task Handle(AddCar request, CancellationToken cancellationToken)
    {
        using var loggerScope = this.logger.BeginScope(
            (CAR_ID, request.Id)
        );

        this.logger.LogInformation("Try to add car");

        var car = await this.repository.GetCarByIdAsync(request.Id);

        if (car is not null)
        {
            throw new CarAlreadyExistsException(request.Id);
        }

        var carEntity = new CarEntity(request.Id, request.Make, request.Model, request.Vin);

        await this.repository.AddCarAsync(carEntity);
    }
}
