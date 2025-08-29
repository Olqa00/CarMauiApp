namespace CarMauiApp.Maui.ViewModels;

using CarMauiApp.Application.Models;
using CarMauiApp.Application.Queries;
using CommunityToolkit.Mvvm.ComponentModel;

[QueryProperty(nameof(NavigationId), "Id")]
public partial class CarDetailsViewModel : BaseViewModel
{
    private readonly ISender? mediator;

    [ObservableProperty]
    private CarModel car;

    [ObservableProperty]
    private Guid id;

    public AsyncRelayCommand GetCarAsyncCommand { get; }

    public string NavigationId
    {
        set
        {
            if (Guid.TryParse(value, out var guid))
            {
                this.id = guid;
                _ = this.GetCarAsync();
            }
        }
    }

    public CarDetailsViewModel(ISender? mediator)
    {
        this.Title = "Car Details";
        this.GetCarAsyncCommand = new AsyncRelayCommand(this.GetCarAsync);
        this.mediator = mediator;
    }

    private async Task GetCarAsync(CancellationToken cancellationToken = default)
    {
        if (this.Id == Guid.Empty)
        {
            return;
        }

        try
        {
            var query = new GetCarById
            {
                Id = this.id,
            };

            var car = await this.mediator.Send(query, cancellationToken);

            this.Car = car;
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Failed to load car details: {ex.Message}", "OK");
        }
    }
}
