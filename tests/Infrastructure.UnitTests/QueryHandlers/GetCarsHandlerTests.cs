namespace CarMauiApp.Infrastructure.UnitTests.QueryHandlers;

using CarMauiApp.Application.Interfaces;
using CarMauiApp.Application.Models;
using CarMauiApp.Application.Queries;
using CarMauiApp.Infrastructure.QueryHandlers;

[TestClass]
public sealed class GetCarsHandlerTests
{
    private const string MAKE_1 = "MAKE_1";
    private const string MAKE_2 = "MAKE_2";
    private const string MODEL_1 = "MODEL_1";
    private const string MODEL_2 = "MODEL_2";
    private const string VIN_1 = "VIN_1";
    private const string VIN_2 = "VIN_2";

    private static readonly Guid ID_1 = Guid.NewGuid();
    private static readonly Guid ID_2 = Guid.NewGuid();

    private static readonly CarModel CAR_MODEL_1 = new()
    {
        Id = ID_1,
        Make = MAKE_1,
        Model = MODEL_1,
        Vin = VIN_1,
    };

    private static readonly CarModel CAR_MODEL_2 = new()
    {
        Id = ID_2,
        Make = MAKE_2,
        Model = MODEL_2,
        Vin = VIN_2,
    };

    private static readonly List<CarModel> CAR_MODELS =
    [
        CAR_MODEL_1,
        CAR_MODEL_2,
    ];

    private readonly GetCarsHandler handler;
    private readonly NullLogger<GetCarsHandler> logger = new();
    private readonly ICarRepository repository = Substitute.For<ICarRepository>();

    public GetCarsHandlerTests()
    {
        this.handler = new GetCarsHandler(this.logger, this.repository);
    }

    [TestMethod]
    public async Task Handle_Should_ReturnCars()
    {
        // Arrange
        this.repository.GetCarsAsync()
            .Returns(CAR_MODELS);

        var query = new GetCars();

        // Act
        var result = await this.handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should()
            .NotBeNull()
            .And
            .HaveCount(2)
            ;
    }

    [TestMethod]
    public async Task Handle_Should_ReturnEmptyList_WhenNoCars()
    {
        // Arrange
        this.repository.GetCarsAsync()
            .Returns(new List<CarModel>());

        var query = new GetCars();

        // Act
        var result = await this.handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should()
            .NotBeNull()
            .And
            .BeEmpty()
            ;
    }
}
