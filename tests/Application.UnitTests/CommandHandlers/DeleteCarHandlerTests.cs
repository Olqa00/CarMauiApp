namespace CarMauiApp.Application.UnitTests.CommandHandlers;

using CarMauiApp.Application.CommandHandlers;
using CarMauiApp.Application.Commands;
using CarMauiApp.Application.Exceptions;
using CarMauiApp.Application.Interfaces;
using CarMauiApp.Application.Models;

[TestClass]
public sealed class DeleteCarHandlerTests
{
    private const string MAKE = "MAKE";
    private const string MODEL = "MODEL";
    private const string VIN = "VIN";
    private static readonly Guid CAR_ID = Guid.NewGuid();
    private readonly DeleteCarHandler handler;
    private readonly NullLogger<DeleteCarHandler> logger = new();
    private readonly ICarRepository repository = Substitute.For<ICarRepository>();

    public DeleteCarHandlerTests()
    {
        this.handler = new DeleteCarHandler(this.logger, this.repository);
    }

    [TestMethod]
    public async Task Handle_ShouldDeleteCar_WhenCarExists()
    {
        // Arrange
        Guid? expectedId = null;

        var existingCar = new CarModel
        {
            Id = CAR_ID,
            Make = MAKE,
            Model = MODEL,
            Vin = VIN,
        };

        this.repository.GetCarByIdAsync(CAR_ID)
            .Returns(existingCar);

        var command = new DeleteCar
        {
            Id = CAR_ID,
        };

        await this.repository.DeleteCarAsync(Arg.Do<Guid>(id => expectedId = id));

        // Act
        await this.handler.Handle(command, CancellationToken.None);

        // Assert
        expectedId.Should()
            .Be(CAR_ID)
            ;
    }

    [TestMethod]
    public async Task Handle_ShouldThrowCarNotFoundException_WhenCarDoesNotExist()
    {
        // Arrange
        this.repository.GetCarByIdAsync(CAR_ID)
            .Returns((CarModel?)null);

        var command = new DeleteCar
        {
            Id = CAR_ID,
        };

        // Act
        var act = async () => await this.handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should()
                .ThrowAsync<CarNotFoundException>()
                .WithMessage($"Car with id '{CAR_ID}' was not found.")
            ;
    }
}
