using System.Windows;
using GTrack_MessageDialogModule.Modules;
using GTrack_Node.ViewModels;
using GTrack_Node.Views;
using GTrack_Services;
using GTrack_Services.Interfaces;

namespace GTrack_Node;

public partial class App : PrismApplication
{
    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterSingleton<MainViewModel>();
        
        containerRegistry.RegisterForNavigation<GTrackNodeView>();
        containerRegistry.RegisterForNavigation<SettingView>();
        
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
        regionManager.RequestNavigate("MainRegion", nameof(GTrackNodeView));
    }
    
    protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
    {
        base.ConfigureModuleCatalog(moduleCatalog);
        moduleCatalog.AddModule<MessageDialogModule>();
    }
}