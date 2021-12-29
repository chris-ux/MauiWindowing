using MauiWindowing.Services;
using System;
using System.Diagnostics;
using System.Linq;
using UIKit;

namespace MauiWindowing.Platforms.Services;

public class DesktopEnvironmentService : IDesktopEnvironmentService
{
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

    private void HandleFullScreenToggle(bool value)
    {
        var scenes = UIApplication.SharedApplication.ConnectedScenes;
        var activeScene = scenes?.ToArray()
            .First(x => x is UIWindowScene && x.ActivationState == UISceneActivationState.ForegroundActive) as UIWindowScene;
        var window = activeScene.Windows.First(x => x.IsKeyWindow) as UIWindow;
        // What object/methods in IOS do we use to trigger fullscreen request?!
        Debug.WriteLine("Handle Full Screen Not Implemented in MacCatalyst -- Chris needs mac device...");
    }

    public void SetWindowProperties()
    {
        Debug.WriteLine("Chris needs mac device...");
    }

    public void UpdateContext(IServiceProvider mauiContextServiceProvider)
    {
        this.mauiContextServiceProvider = mauiContextServiceProvider;
    }
}



