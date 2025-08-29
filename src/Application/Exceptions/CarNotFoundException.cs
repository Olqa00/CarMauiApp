namespace CarMauiApp.Application.Exceptions;

public sealed class CarNotFoundException : ApplicationException
{
    public CarNotFoundException(Guid id)
        : base($"Car with id '{id}' was not found.")
    {
        this.Id = id;
    }
}
