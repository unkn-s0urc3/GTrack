using GTrack_Control.Events;
using GTrack_Control.Views;

namespace GTrack_Control.ViewModels;

public class SettingViewModel : BindableBase, INavigationAware
{
    private readonly IRegionManager _regionManager;
    private readonly IEventAggregator _eventAggregator;

    public DelegateCommand NavigateToGTrackControlViewCommand { get; }
    public DelegateCommand SendMessageEventCommand { get; }

    public SettingViewModel(IRegionManager regionManager, 
        IEventAggregator eventAggregator)
    {
        _regionManager = regionManager;
        _eventAggregator = eventAggregator;
        
        NavigateToGTrackControlViewCommand = new DelegateCommand(Navigate);
        SendMessageEventCommand = new DelegateCommand(SendMessageEvent);
    }

    private void Navigate()
    {
        _regionManager.RequestNavigate("MainRegion", nameof(GTrackControlView));
    }
    
    private void SendMessageEvent()
    {
        _eventAggregator.GetEvent<AppMessageEvent>().Publish("Hello from Setting!");
    }

    public void OnNavigatedTo(NavigationContext navigationContext) { }
    public bool IsNavigationTarget(NavigationContext navigationContext) => true;
    public void OnNavigatedFrom(NavigationContext navigationContext) { }
}