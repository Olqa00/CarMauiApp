namespace CarMauiApp.Domain.Entities;

using CarMauiApp.Domain.Exceptions;

public sealed class CarEntity
{
    public Guid Id { get; private set; }
    public string Make { get; private set; }
    public string Model { get; private set; }
    public string Vin { get; private set; }

    public CarEntity(Guid id, string make, string model, string vin)
    {
        this.Id = id;
        this.SetMake(make);
        this.SetModel(model);
        this.SetVin(vin);
    }

    public void SetMake(string make)
    {
        if (string.IsNullOrWhiteSpace(make))
        {
            throw new CarEmptyMakeException(this.Id);
        }

        this.Make = make;
    }

    public void SetModel(string model)
    {
        if (string.IsNullOrWhiteSpace(model))
        {
            throw new CarEmptyModelException(this.Id);
        }

        this.Model = model;
    }

    public void SetVin(string vin)
    {
        if (string.IsNullOrWhiteSpace(vin))
        {
            throw new CarEmptyVinException(this.Id);
        }

        this.Vin = vin;
    }
}
