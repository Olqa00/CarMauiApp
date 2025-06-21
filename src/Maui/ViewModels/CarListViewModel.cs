namespace CarMauiApp.Maui.ViewModels;

using System.Collections.ObjectModel;
using System.Diagnostics;
using CarMauiApp.Application.Interfaces;
using CarMauiApp.Application.Models;

public class CarListViewModel : BaseViewModel
{
    private readonly ICarService carService;
    public ObservableCollection<CarModel> Cars { get; private set; } = [];

    public CarListViewModel(ICarService carService)
    {
        this.Title = "Car List";
        this.carService = carService;
    }

    public async Task GetCarListAsync()
    {
        if (this.IsBusy)
        {
            return;
        }

        try
        {
            this.IsBusy = true;

            if (this.Cars.Any() is true)
            {
                this.Cars.Clear();
            }

            var cars = await Task.Run(() => this.carService.GetCars()); //TODO CQRS
            this.Cars = new ObservableCollection<CarModel>(cars);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"An error occurred while fetching the car list: {ex.Message}");

            await Shell.Current.DisplayAlert(
                "Error",
                "An error occurred while fetching the car list. Please try again later.",
                "OK");
        }
        finally
        {
            this.IsBusy = false;
        }
    }
}
