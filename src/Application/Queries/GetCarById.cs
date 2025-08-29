namespace CarMauiApp.Application.Queries;

using CarMauiApp.Application.Models;

public sealed record class GetCarById : IRequest<CarModel>
{
    public required Guid Id { get; init; }
}
