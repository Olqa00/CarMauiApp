namespace CarMauiApp.Maui;

using CarMauiApp.Application;
using CarMauiApp.Infrastructure;
using CarMauiApp.Maui.ViewModels;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services
            .AddApplication()
            .AddInfrastructure(builder.Configuration);

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<CarListViewModel>(); //TODO Move

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
