namespace CarMauiApp.Domain.Exceptions;

public sealed class CarEmptyMakeException : DomainException
{
    public CarEmptyMakeException(Guid id)
        : base($"Car make can not be empty. Car id: {id}")
    {
        this.Id = id;
    }
}
