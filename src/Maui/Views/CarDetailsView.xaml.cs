namespace CarMauiApp.Maui.Views;

using CarMauiApp.Maui.ViewModels;

public partial class CarDetailsView : ContentPage
{
    public CarDetailsView(CarDetailsViewModel carDetailsViewModel)
    {
        this.InitializeComponent();
        this.BindingContext = carDetailsViewModel;
    }
}
