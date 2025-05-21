using System.Collections.ObjectModel;
using GTrack_Events;
using GTrack_Events.Messages;
using GTrack_Node.Models;

namespace GTrack_Node.ViewModels;

public class GTrackNodeViewModel : BindableBase, INavigationAware
{
    private readonly IRegionManager _regionManager;
    private readonly IEventAggregator _eventAggregator;
    private readonly IDialogService _dialogService;
    
    private readonly ObservationParameterModel _observationModel;
    
    private string _stationStatus;
    public string StationStatus
    {
        get => _stationStatus;
        set => SetProperty(ref _stationStatus, value);
    }

    private string _controlStatus;
    public string ControlStatus
    {
        get => _controlStatus;
        set => SetProperty(ref _controlStatus, value);
    }
    
    private ObservableCollection<string> _connectedStations;
    public ObservableCollection<string> ConnectedStations
    {
        get => _connectedStations;
        set => SetProperty(ref _connectedStations, value);
    }
    
    private ObservableCollection<ObservationParameter> _observationParameters;
    public ObservableCollection<ObservationParameter> ObservationParameters
    {
        get => _observationParameters;
        set => SetProperty(ref _observationParameters, value);
    }
    
    public GTrackNodeViewModel(IRegionManager regionManager, 
        IEventAggregator eventAggregator, IDialogService dialogService)
    {
        _regionManager = regionManager;
        _eventAggregator = eventAggregator;
        _dialogService = dialogService;
        
        _observationModel = new ObservationParameterModel();

        ObservationParameters = _observationModel.Parameters;
        ConnectedStations = new ObservableCollection<string>
        {
            "Station 1", "Station 2", "Station 3"
        };
        
        _eventAggregator.GetEvent<AppMessageEvent>().Subscribe(OnServerStatusChanged);
    }
    
    private void OnServerStatusChanged(ServerStatusMessage message)
    {
        if (message.ServerName == "Control")
        {
            ControlStatus = message.Status;
        }
        else if (message.ServerName == "Station")
        {
            StationStatus = message.Status;
        }
    }
    
    public void OnNavigatedTo(NavigationContext navigationContext) { }
    public bool IsNavigationTarget(NavigationContext navigationContext) => true;
    public void OnNavigatedFrom(NavigationContext navigationContext) { }
}