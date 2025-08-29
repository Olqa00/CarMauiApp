namespace CarMauiApp.Application.UnitTests.CommandHandlers;

using CarMauiApp.Application.CommandHandlers;
using CarMauiApp.Application.Commands;
using CarMauiApp.Application.Exceptions;
using CarMauiApp.Application.Interfaces;
using CarMauiApp.Application.Models;
using CarMauiApp.Domain.Entities;

[TestClass]
public sealed class AddCarHandlerTests
{
    private const string MAKE = "MAKE";
    private const string MODEL = "MODEL";
    private const string VIN = "VIN";
    private static readonly Guid CAR_ID = Guid.NewGuid();
    private readonly AddCarHandler handler;
    private readonly NullLogger<AddCarHandler> logger = new();
    private readonly ICarRepository repository = Substitute.For<ICarRepository>();

    public AddCarHandlerTests()
    {
        this.handler = new AddCarHandler(this.logger, this.repository);
    }

    [TestMethod]
    public async Task Handle_ShouldAddCar_WhenCarDoesNotExist()
    {
        // Arrange
        CarEntity? carEntity = null;

        this.repository.GetCarByIdAsync(CAR_ID)
            .Returns((CarModel?)null);

        var command = new AddCar
        {
            Id = CAR_ID,
            Make = MAKE,
            Model = MODEL,
            Vin = VIN,
        };

        await this.repository.AddCarAsync(Arg.Do<CarEntity>(car => carEntity = car));

        // Act
        await this.handler.Handle(command, CancellationToken.None);

        // Assert
        var expectedEntity = new CarEntity(CAR_ID, MAKE, MODEL, VIN);

        carEntity.Should()
            .BeEquivalentTo(expectedEntity)
            ;
    }

    [TestMethod]
    public async Task Handle_ShouldThrowCarAlreadyExistsException_WhenCarExists()
    {
        // Arrange
        var existingCar = new CarModel
        {
            Id = CAR_ID,
            Make = MAKE,
            Model = MODEL,
            Vin = VIN,
        };

        this.repository.GetCarByIdAsync(CAR_ID)
            .Returns(existingCar);

        var command = new AddCar
        {
            Id = CAR_ID,
            Make = MAKE,
            Model = MODEL,
            Vin = VIN,
        };

        // Act
        var act = async () => await this.handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should()
                .ThrowAsync<CarAlreadyExistsException>()
                .WithMessage($"Car with id '{CAR_ID}' already exists.")
            ;
    }
}
