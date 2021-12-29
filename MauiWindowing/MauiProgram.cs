using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MauiWindowing.Services;

namespace MauiWindowing;

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
            })
            .ConfigureServices()
            .ConfigurePlatformServices();

        DependencyService.ServiceProvider = builder.Services.BuildServiceProvider();
        return builder.Build();
    }
}

public static class DependencyService {
    public static ServiceProvider ServiceProvider;

    public static MauiAppBuilder ConfigureServices(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainPageViewModel>();

        return builder;
    }

    /// <summary>
    /// These are platform specific services that may not have implementations on all targets.
    /// <br/>Do not use constructor injection with these.
    /// <br/>Use ServiceProvider.GetService≤T≥() to retrieve dependencies.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static MauiAppBuilder ConfigurePlatformServices(this MauiAppBuilder builder) {
#if WINDOWS || MACCATALYST
        builder.Services.AddSingleton<IDesktopEnvironmentService, Platforms.Services.DesktopEnvironmentService>();
#endif
        return builder;
    }
}