namespace CarMauiApp.Maui.ViewModels;

using CarMauiApp.Application.Interfaces;
using CarMauiApp.Application.Models;
using CarMauiApp.Application.Queries;
using CommunityToolkit.Mvvm.ComponentModel;

public partial class CarListViewModel : BaseViewModel
{
    private readonly ICarService carService;
    private readonly ISender? mediator;

    [ObservableProperty]
    private bool isRefreshing;

    public ObservableCollection<CarModel> Cars { get; private set; } = [];

    public AsyncRelayCommand GetCarListAsyncCommand { get; }

    public CarListViewModel(ICarService carService, ISender? mediator)
    {
        this.Title = "Car List";
        this.mediator = mediator;
        this.carService = carService;
        this.GetCarListAsyncCommand = new AsyncRelayCommand(this.GetCarListAsync);
    }

    private async Task GetCarListAsync(CancellationToken cancellationToken = default)
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
            var carModels = new ObservableCollection<CarModel>(cars);

            foreach (var car in carModels)
            {
                this.Cars.Add(car);
            }
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
