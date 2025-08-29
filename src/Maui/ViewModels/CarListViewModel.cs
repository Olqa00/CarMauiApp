namespace CarMauiApp.Maui.ViewModels;

using CarMauiApp.Application.Commands;
using CarMauiApp.Application.Models;
using CarMauiApp.Application.Queries;
using CarMauiApp.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;

public partial class CarListViewModel : BaseViewModel
{
    private readonly ISender? mediator;

    [ObservableProperty]
    private bool isRefreshing;

    [ObservableProperty]
    private string make;

    [ObservableProperty]
    private string model;

    [ObservableProperty]
    private string vin;

    public AsyncRelayCommand AddCarAsyncCommand { get; }

    public ObservableCollection<CarModel> Cars { get; private set; } = [];
    public AsyncRelayCommand<Guid> DeleteCarAsyncCommand { get; }
    public AsyncRelayCommand<Guid> GetCarDetailsAsyncCommand { get; }

    public AsyncRelayCommand GetCarListAsyncCommand { get; }

    public CarListViewModel(ISender? mediator)
    {
        this.Title = "Car List";
        this.mediator = mediator;
        this.AddCarAsyncCommand = new AsyncRelayCommand(this.AddCarAsync);
        this.DeleteCarAsyncCommand = new AsyncRelayCommand<Guid>(this.DeleteCarAsync);
        this.GetCarDetailsAsyncCommand = new AsyncRelayCommand<Guid>(this.GetCarDetailsAsync);
        this.GetCarListAsyncCommand = new AsyncRelayCommand(this.GetCarListAsync);
    }

    private async Task AddCarAsync(CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(this.Make) || string.IsNullOrEmpty(this.Model) || string.IsNullOrEmpty(this.Vin))
        {
            await Shell.Current.DisplayAlert(
                "Validation Error",
                "Make, Model, and VIN are required fields.",
                "OK");

            return;
        }

        try
        {
            var id = Guid.NewGuid();

            var command = new AddCar
            {
                Id = id,
                Make = this.Make,
                Model = this.Model,
                Vin = this.Vin,
            };

            await this.mediator.Send(command, cancellationToken);

            this.Make = string.Empty;
            this.Model = string.Empty;
            this.Vin = string.Empty;

            await Shell.Current.DisplayAlert(
                "Info",
                "Car added successfully.",
                "OK");

            await this.GetCarListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"An error occurred while adding a new car: {ex.Message}");

            await Shell.Current.DisplayAlert(
                "Error",
                "An error occurred while adding the car. Please try again later.",
                "OK");
        }
    }

    private async Task DeleteCarAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            await Shell.Current.DisplayAlert(
                "Validation Error",
                "Id is required field.",
                "OK");
        }

        try
        {
            var command = new DeleteCar
            {
                Id = id,
            };

            this.mediator.Send(command, cancellationToken);

            await Shell.Current.DisplayAlert(
                "Info",
                "Car deleted successfully.",
                "OK");

            await this.GetCarListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"An error occurred while deleting a car: {ex.Message}");

            await Shell.Current.DisplayAlert(
                "Error",
                "An error occurred while deleting the car. Please try again later.",
                "OK");
        }
    }

    private async Task GetCarDetailsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            return;
        }

        await Shell.Current.GoToAsync($"{nameof(CarDetailsView)}?Id={id}", animate: true);
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
