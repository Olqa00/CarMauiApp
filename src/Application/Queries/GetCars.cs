namespace CarMauiApp.Application.Queries;

using CarMauiApp.Application.Models;

public sealed class GetCars : IRequest<IReadOnlyList<CarModel>>
{
}
