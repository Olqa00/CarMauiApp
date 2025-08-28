namespace CarMauiApp.Maui;

using CarMauiApp.Application;
using CarMauiApp.Infrastructure;
using CarMauiApp.Maui.ViewModels;
using CarMauiApp.Maui.Views;
using Microsoft.Extensions.Configuration;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<App>()
            .Build();

        builder.Configuration.AddConfiguration(config);

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

        builder.Services.AddTransient<CarListViewModel>();
        builder.Services.AddTransient<CarDetailsViewModel>();
        builder.Services.AddTransient<MainPage>(); //TODO Move
        builder.Services.AddTransient<CarDetailsView>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
