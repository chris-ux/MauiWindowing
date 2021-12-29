using System;

namespace MauiWindowing.Services;

public interface IDesktopEnvironmentService
{
    public bool IsFullScreen { get; set; }
    public void UpdateContext(IServiceProvider mauiContextServiceProvider);
}

