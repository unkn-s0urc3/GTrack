using System.Collections.ObjectModel;
using GTrack_Control.Events;
using GTrack_Control.Services.Interfaces;
using GTrack_Control.Views;

namespace GTrack_Control.ViewModels;

public class GTrackControlViewModel : BindableBase, INavigationAware
{
    private readonly IRegionManager _regionManager;
    private readonly IEventAggregator _eventAggregator;
    private readonly IDialogService _dialogService;

    private string _nodeServerStatus;
    public string NodeServerStatus
    {
        get => _nodeServerStatus;
        set => SetProperty(ref _nodeServerStatus, value);
    }

    private string _houstonServerStatus;
    public string HoustonServerStatus
    {
        get => _houstonServerStatus;
        set => SetProperty(ref _houstonServerStatus, value);
    }

    private ObservableCollection<string> _gTrackStations;
    public ObservableCollection<string> GTrackStations
    {
        get => _gTrackStations;
        set => SetProperty(ref _gTrackStations, value);
    }

    private string _selectedGTrackStation;
    public string SelectedGTrackStation
    {
        get => _selectedGTrackStation;
        set => SetProperty(ref _selectedGTrackStation, value);
    }

    private string _currentObservation;
    public string CurrentObservation
    {
        get => _currentObservation;
        set => SetProperty(ref _currentObservation, value);
    }
    
    public DelegateCommand AcceptCommand { get; }
    public DelegateCommand NavigateToSettingViewCommand { get; }

    public GTrackControlViewModel(IRegionManager regionManager, 
        IEventAggregator eventAggregator, IDialogService dialogService)
    {
        _regionManager = regionManager;
        _eventAggregator = eventAggregator;
        _dialogService = dialogService;
        
        AcceptCommand = new DelegateCommand(OnAccept);
        NavigateToSettingViewCommand = new DelegateCommand(Navigate);
        
        _eventAggregator.GetEvent<AppMessageEvent>().Subscribe(OnServerStatusChanged);
        
        // variables
        GTrackStations = new ObservableCollection<string>
        {
            "Station 1", "Station 2", "Station 3"
        };
        CurrentObservation = "ReshUCube-2";
    }

    private void OnAccept()
    {
        
    }
    
    private void Navigate()
    {
        _regionManager.RequestNavigate("MainRegion", nameof(SettingView));
    }
    
    private void OnServerStatusChanged(ServerStatusMessage message)
    {
        if (message.ServerName == "Node")
        {
            NodeServerStatus = message.Status;
        }
        else if (message.ServerName == "Houston")
        {
            HoustonServerStatus = message.Status;
        }
    }

    public void OnNavigatedTo(NavigationContext navigationContext) { }
    public bool IsNavigationTarget(NavigationContext navigationContext) => true;
    public void OnNavigatedFrom(NavigationContext navigationContext) { }
}