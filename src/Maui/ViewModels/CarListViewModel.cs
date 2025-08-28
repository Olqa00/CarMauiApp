namespace CarMauiApp.Maui.ViewModels;

using CarMauiApp.Application.Interfaces;
using CarMauiApp.Application.Models;
using CarMauiApp.Application.Queries;
using CarMauiApp.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;

public partial class CarListViewModel : BaseViewModel
{
    private readonly ICarRepository carService;
    private readonly ISender? mediator;

    [ObservableProperty]
    private bool isRefreshing;

    public ObservableCollection<CarModel> Cars { get; private set; } = [];
    public AsyncRelayCommand<CarModel> GetCarDetailsAsyncCommand { get; }

    public AsyncRelayCommand GetCarListAsyncCommand { get; }

    public CarListViewModel(ICarRepository carService, ISender? mediator)
    {
        this.Title = "Car List";
        this.mediator = mediator;
        this.carService = carService;
        this.GetCarDetailsAsyncCommand = new AsyncRelayCommand<CarModel>(this.GetCarDetailsAsync);
        this.GetCarListAsyncCommand = new AsyncRelayCommand(this.GetCarListAsync);
    }

    private async Task GetCarDetailsAsync(CarModel? carModel, CancellationToken cancellationToken = default)
    {
        if (carModel is null)
        {
            return;
        }

        await Shell.Current.GoToAsync(nameof(CarDetailsView),
            animate: true,
            new Dictionary<string, object>
            {
                { nameof(CarModel), carModel },
            });
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
