namespace CarMauiApp.Infrastructure.UnitTests.QueryHandlers;

using CarMauiApp.Application.Interfaces;
using CarMauiApp.Application.Models;
using CarMauiApp.Application.Queries;
using CarMauiApp.Infrastructure.Exceptions;
using CarMauiApp.Infrastructure.QueryHandlers;

[TestClass]
public sealed class GetCarByIdHandlerTests
{
    private const string MAKE = "MAKE";
    private const string MODEL = "MODEL";
    private const string VIN = "VIN";

    private static readonly Guid ID = Guid.NewGuid();

    private static readonly CarModel CAR_MODEL = new()
    {
        Id = ID,
        Make = MAKE,
        Model = MODEL,
        Vin = VIN,
    };

    private readonly GetCarByIdHandler handler;
    private readonly NullLogger<GetCarByIdHandler> logger = new();
    private readonly ICarRepository repository = Substitute.For<ICarRepository>();

    public GetCarByIdHandlerTests()
    {
        this.handler = new GetCarByIdHandler(this.logger, this.repository);
    }

    [TestMethod]
    public async Task Handle_Should_ReturnCar()
    {
        // Arrange
        this.repository.GetCarByIdAsync(ID)
            .Returns(CAR_MODEL);

        var query = new GetCarById
        {
            Id = ID,
        };

        // Act
        var result = await this.handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should()
            .BeEquivalentTo(CAR_MODEL)
            ;
    }

    [TestMethod]
    public async Task Handle_Should_ThrowCarNotFoundException()
    {
        // Arrange
        this.repository.GetCarByIdAsync(ID)
            .Returns((CarModel?)null);

        var query = new GetCarById
        {
            Id = ID,
        };

        // Act
        var act = async () => await this.handler.Handle(query, CancellationToken.None);

        // Assert
        await act.Should()
                .ThrowAsync<CarNotFoundException>()
                .WithMessage($"Car with id '{ID}' was not found.")
            ;
    }
}
