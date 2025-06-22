namespace CarMauiApp.Maui.ViewModels;

using System.Collections.ObjectModel;
using System.Diagnostics;
using CarMauiApp.Application.Interfaces;
using CarMauiApp.Application.Models;
using CarMauiApp.Application.Queries;
using MediatR;

public class CarListViewModel : BaseViewModel
{
    private readonly ICarService carService;
    private readonly ISender? mediator;
    public ObservableCollection<CarModel> Cars { get; private set; } = [];

    public CarListViewModel(ICarService carService, ISender? mediator)
    {
        this.Title = "Car List";
        this.mediator = mediator;
        this.carService = carService;
    }

    public async Task GetCarListAsync(CancellationToken cancellationToken = default)
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

            var query = new GetCars();

            var cars = await this.mediator.Send(query, cancellationToken);
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
