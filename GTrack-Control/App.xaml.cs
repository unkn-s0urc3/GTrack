using System.Windows;
using GTrack_Control.Services;
using GTrack_Control.Services.Interfaces;
using GTrack_Control.ViewModels;
using GTrack_Control.Views;

namespace GTrack_Control;

public partial class App : PrismApplication
{
    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    { 
        containerRegistry.RegisterSingleton<MainViewModel>();
        
        containerRegistry.RegisterForNavigation<GTrackControlView>();
        containerRegistry.RegisterForNavigation<SettingView>();
        
        containerRegistry.RegisterDialog<MessageDialogView, MessageDialogViewModel>();
        
        containerRegistry.Register<IFileDialogService, FileDialogService>();
        containerRegistry.Register<IApplicationService, ApplicationService>();
        containerRegistry.RegisterSingleton<INetworkValidationService, NetworkValidationService>();
    }

    protected override Window CreateShell()
    {
        return ContainerLocator.Container.Resolve<MainView>();
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        
        var regionManager = Container.Resolve<IRegionManager>();
        regionManager.RequestNavigate("MainRegion", nameof(GTrackControlView));
    }
}