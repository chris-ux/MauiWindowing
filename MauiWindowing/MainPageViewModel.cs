
using System.Windows.Input;
using MauiWindowing.Services;
using Microsoft.Extensions.DependencyInjection;
using MvvmHelpers;
using MvvmHelpers.Commands;

namespace MauiWindowing;

public class MainPageViewModel : ObservableObject
{
    public ICommand FullScreenToggleCommand { get; private set; }

    public MainPageViewModel()
    {
        FullScreenToggleCommand = new Command(ToggleFullScreenAction);
    }

    private void ToggleFullScreenAction(object obj)
    {
        IDesktopEnvironmentService windowHelperService = DependencyService.ServiceProvider.GetService<IDesktopEnvironmentService>();
        if (windowHelperService != null)
            windowHelperService.IsFullScreen = !windowHelperService.IsFullScreen;
    }
}

