using GTrack_MessageDialogModule.ViewModels;
using GTrack_MessageDialogModule.Views;

namespace GTrack_MessageDialogModule.Modules;

public class MessageDialogModule : IModule
{
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterDialog<MessageDialogView, MessageDialogViewModel>();
    }

    public void OnInitialized(IContainerProvider containerProvider) { }
}