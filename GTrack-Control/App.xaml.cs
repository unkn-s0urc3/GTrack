using System.Windows;
using GTrack_Control.Views;

namespace GTrack_Control;

public partial class App : PrismApplication
{
    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        
    }

    protected override Window CreateShell()
    {
        return ContainerLocator.Container.Resolve<MainView>();
    }
}