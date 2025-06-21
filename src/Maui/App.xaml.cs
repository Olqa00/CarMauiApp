namespace CarMauiApp.Maui;

using Application = Microsoft.Maui.Controls.Application;

public partial class App : Application
{
    public App()
    {
        this.InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}
