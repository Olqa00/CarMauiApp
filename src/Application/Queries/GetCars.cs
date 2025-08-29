namespace CarMauiApp.Application.Queries;

using CarMauiApp.Application.Models;

public sealed record class GetCars : IRequest<IReadOnlyList<CarModel>>
{
}
