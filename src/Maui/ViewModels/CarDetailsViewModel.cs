namespace CarMauiApp.Maui.ViewModels;

using CarMauiApp.Application.Models;
using CommunityToolkit.Mvvm.ComponentModel;

[QueryProperty(nameof(CarModel), nameof(CarModel))]
public partial class CarDetailsViewModel : BaseViewModel
{
    [ObservableProperty]
    private CarModel carModel;

    public CarDetailsViewModel()
    {
        this.Title = "Car Details";
    }
}
