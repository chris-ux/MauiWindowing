using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using Microsoft.UI;
using Microsoft.UI.Windowing;

using MauiWindowing.Services;
using System.Diagnostics;

namespace MauiWindowing.Platforms.Services;

public class DesktopEnvironmentService : IDesktopEnvironmentService
{
    private AppWindow AppWindow { get; set; }
    private IServiceProvider mauiContextServiceProvider;

    public DesktopEnvironmentService() { }

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

    public void UpdateContext(IServiceProvider mauiContextServiceProvider)
    {
        this.mauiContextServiceProvider = mauiContextServiceProvider;
    }

    // https://docs.microsoft.com/en-us/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.fullscreenpresenter?view=windows-app-sdk-1.0
    private void HandleFullScreenToggle(bool value)
    {
        var appWindow = GetAppWindow();
        if (appWindow is null) { Debug.WriteLine("appWindow is null at HandleFullScreenToggle (Windows)"); return; }

        if (value)
            appWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
        else
            appWindow.SetPresenter(AppWindowPresenterKind.Default);
    }

    // https://docs.microsoft.com/en-us/windows/apps/windows-app-sdk/windowing/windowing-overvie
    private AppWindow GetAppWindow()
    {
        Microsoft.UI.Xaml.Window window = mauiContextServiceProvider.GetService<Microsoft.UI.Xaml.Window>();
        IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
        WindowId myWndId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
        AppWindow = AppWindow.GetFromWindowId(myWndId);
        return AppWindow;
    }

}

