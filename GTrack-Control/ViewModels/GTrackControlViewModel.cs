using GTrack_Control.Events;
using GTrack_Control.Views;

namespace GTrack_Control.ViewModels;

public class GTrackControlViewModel : BindableBase, INavigationAware
{
    private readonly IRegionManager _regionManager;
    private readonly IEventAggregator _eventAggregator;

    public DelegateCommand NavigateToSettingViewCommand { get; }
    public DelegateCommand SendMessageEventCommand { get; }

    public GTrackControlViewModel(IRegionManager regionManager, 
        IEventAggregator eventAggregator)
    {
        _regionManager = regionManager;
        _eventAggregator = eventAggregator;
        
        NavigateToSettingViewCommand = new DelegateCommand(Navigate);
        SendMessageEventCommand = new DelegateCommand(SendMessageEvent);
    }

    private void Navigate()
    {
        _regionManager.RequestNavigate("MainRegion", nameof(SettingView));
    }
    
    private void SendMessageEvent()
    {
        _eventAggregator.GetEvent<AppMessageEvent>().Publish("Hello from GTrackControl!");
    }

    public void OnNavigatedTo(NavigationContext navigationContext) { }
    public bool IsNavigationTarget(NavigationContext navigationContext) => true;
    public void OnNavigatedFrom(NavigationContext navigationContext) { }
}