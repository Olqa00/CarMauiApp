namespace CarMauiApp.Application.Commands;

public sealed record class AddCar : IRequest
{
    public required Guid Id { get; init; }
    public required string Make { get; init; }
    public required string Model { get; init; }
    public required string Vin { get; init; }
}
