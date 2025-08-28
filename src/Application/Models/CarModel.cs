namespace CarMauiApp.Application.Models;

public sealed record class CarModel
{
    public Guid Id { get; set; }
    public string Make { get; init; } = string.Empty;
    public string Model { get; init; } = string.Empty;
    public string Vin { get; init; } = string.Empty;
}
