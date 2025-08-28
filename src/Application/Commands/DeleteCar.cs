namespace CarMauiApp.Application.Commands;

public sealed record class DeleteCar : IRequest
{
    public required Guid Id { get; init; }
}
