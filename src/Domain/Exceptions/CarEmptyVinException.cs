namespace CarMauiApp.Domain.Exceptions;

public sealed class CarEmptyVinException : DomainException
{
    public CarEmptyVinException(Guid id)
        : base($"Car Vin can not be empty. Car id: {id}")
    {
        this.Id = id;
    }
}
