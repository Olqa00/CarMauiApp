namespace CarMauiApp.Maui;

using CarMauiApp.Maui.Views;

public partial class AppShell : Shell
{
    public AppShell()
    {
        this.InitializeComponent();

        Routing.RegisterRoute(nameof(CarDetailsView), typeof(CarDetailsView));
    }
}
