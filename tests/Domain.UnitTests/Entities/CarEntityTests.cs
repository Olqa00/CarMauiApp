namespace CarMauiApp.Domain.UnitTests.Entities;

using CarMauiApp.Domain.Entities;
using CarMauiApp.Domain.Exceptions;

[TestClass]
public sealed class CarEntityTests
{
    private const string MAKE = "Toyota";
    private const string MODEL = "Corolla";
    private const string NEW_MAKE = "Honda";
    private const string NEW_MODEL = "Civic";
    private const string NEW_VIN = "2HGBH41JXMN109186";
    private const string VIN = "1HGBH41JXMN109186";
    private static readonly Guid CAR_ID = Guid.NewGuid();

    [TestMethod]
    public void Constructor_Should_SetProperties()
    {
        // Arrange

        // Act
        var carEntity = new CarEntity(CAR_ID, MAKE, MODEL, VIN);

        // Assert
        carEntity.Id.Should()
            .Be(CAR_ID)
            ;

        carEntity.Make.Should()
            .Be(MAKE)
            ;

        carEntity.Model.Should()
            .Be(MODEL)
            ;

        carEntity.Vin.Should()
            .Be(VIN)
            ;
    }

    [TestMethod]
    public void SetMake_Should_SetMake()
    {
        // Arrange
        var carEntity = new CarEntity(CAR_ID, MAKE, MODEL, VIN);

        // Act
        carEntity.SetMake(NEW_MAKE);

        // Assert

        carEntity.Make.Should()
            .Be(NEW_MAKE)
            ;
    }

    [DataTestMethod, DataRow(""), DataRow("            ")]
    public void SetMake_Show_ThrowCarEmptyMakeException(string make)
    {
        // Arrange

        // Act
        var action = () => new CarEntity(CAR_ID, make, MODEL, VIN);

        // Assert
        action.Should()
            .Throw<CarEmptyMakeException>()
            ;
    }

    [TestMethod]
    public void SetModel_Should_SetModel()
    {
        // Arrange
        var carEntity = new CarEntity(CAR_ID, MAKE, MODEL, VIN);

        // Act
        carEntity.SetModel(NEW_MODEL);

        // Assert
        carEntity.Model.Should()
            .Be(NEW_MODEL)
            ;
    }

    [DataTestMethod, DataRow(""), DataRow("            ")]
    public void SetModel_Show_ThrowCarEmptyModelException(string model)
    {
        // Arrange

        // Act
        var action = () => new CarEntity(CAR_ID, MAKE, model, VIN);

        // Assert
        action.Should()
            .Throw<CarEmptyModelException>()
            ;
    }

    [TestMethod]
    public void SetVin_Should_SetVin()
    {
        // Arrange
        var carEntity = new CarEntity(CAR_ID, MAKE, MODEL, VIN);

        // Act
        carEntity.SetVin(NEW_VIN);

        // Assert
        carEntity.Vin.Should()
            .Be(NEW_VIN)
            ;
    }

    [DataTestMethod, DataRow(""), DataRow("            ")]
    public void SetVin_Show_ThrowCarEmptyVinException(string vin)
    {
        // Arrange

        // Act
        var action = () => new CarEntity(CAR_ID, MAKE, MODEL, vin);

        // Assert
        action.Should()
            .Throw<CarEmptyVinException>()
            ;
    }
}
