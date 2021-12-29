using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using Microsoft.UI;
using Microsoft.UI.Windowing;

using MauiWindowing.Services;

namespace MauiWindowing.Platforms.Services;

public class DesktopEnvironmentService : IDesktopEnvironmentService
{
    private AppWindow AppWindow { get; set; }
    private IServiceProvider mauiContextServiceProvider;

    public DesktopEnvironmentService()
    {
    }

    private bool isFullScreen;
    public bool IsFullScreen
    {
        get => isFullScreen;
        set
        {
            HandleFullScreenToggle(value);
            isFullScreen = value;
        }
    }

    private void HandleFullScreenToggle(bool value)
    {
        var appWindow = GetAppWindow();
        if (value)
            appWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
        else
            appWindow.SetPresenter(AppWindowPresenterKind.Default);
    }

    private AppWindow GetAppWindow()
    {
        Microsoft.UI.Xaml.Window window = mauiContextServiceProvider.GetService<Microsoft.UI.Xaml.Window>();
        IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
        WindowId myWndId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
        AppWindow = AppWindow.GetFromWindowId(myWndId);
        return AppWindow;
    }

    public void UpdateContext(IServiceProvider mauiContextServiceProvider)
    {
        this.mauiContextServiceProvider = mauiContextServiceProvider;
    }
}

