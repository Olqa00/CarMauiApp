namespace CarMauiApp.Application.CommandHandlers;

using CarMauiApp.Application.Commands;
using CarMauiApp.Application.Exceptions;
using CarMauiApp.Application.Interfaces;

internal sealed class DeleteCarHandler : IRequestHandler<DeleteCar>
{
    private const string CAR_ID = "carId";
    private readonly ILogger<DeleteCarHandler> logger;
    private readonly ICarRepository repository;

    public DeleteCarHandler(ILogger<DeleteCarHandler> logger, ICarRepository repository)
    {
        this.logger = logger;
        this.repository = repository;
    }

    public async Task Handle(DeleteCar request, CancellationToken cancellationToken)
    {
        using var loggerScope = this.logger.BeginScope(
            (CAR_ID, request.Id)
        );

        this.logger.LogInformation("Try to delete car");

        var car = await this.repository.GetCarByIdAsync(request.Id);

        if (car is null)
        {
            throw new CarNotFoundException(request.Id);
        }

        await this.repository.DeleteCarAsync(request.Id);
    }
}
