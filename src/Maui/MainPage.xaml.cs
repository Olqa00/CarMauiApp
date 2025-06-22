namespace CarMauiApp.Maui;

using CarMauiApp.Maui.ViewModels;

public partial class MainPage : ContentPage
{
    public MainPage(CarListViewModel carListViewModel)
    {
        this.InitializeComponent();
        this.BindingContext = carListViewModel;
    }
}
