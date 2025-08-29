namespace CarMauiApp.Application.Exceptions;

public sealed class CarAlreadyExistsException : ApplicationException
{
    public CarAlreadyExistsException(Guid id)
        : base($"Car with id '{id}' already exists")
    {
        this.Id = id;
    }
}
