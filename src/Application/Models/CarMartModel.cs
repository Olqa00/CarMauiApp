namespace CarMauiApp.Application.Models;

public sealed record class CarMartModel
{
    public List<CarModel> Cars { get; set; } = [];
    public int Id { get; set; }
}
