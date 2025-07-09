namespace CarMauiApp.Domain.Exceptions;

public sealed class CarEmptyModelException : DomainException
{
    public CarEmptyModelException(Guid id)
        : base($"Car model can not be empty. Car id: {id}")
    {
        this.Id = id;
    }
}
