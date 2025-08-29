namespace CarMauiApp.Infrastructure.Exceptions;

public sealed class CarNotFoundException : InfrastructureException
{
    public CarNotFoundException(Guid id)
        : base($"Car with id '{id}' was not found.")
    {
        this.Id = id;
    }
}
