using System.Collections.ObjectModel;
using GTrack_Events;
using GTrack_Events.Messages;
using GTrack_MessageDialogModule.Views;
using GTrack_Node.Models;
using GTrack_Node.Views;
using GTrack_Services.Interfaces;

namespace GTrack_Node.ViewModels;

public class SettingViewModel : BindableBase, INavigationAware
{
    private readonly IRegionManager _regionManager;
    private readonly IDialogService _dialogService;
    private readonly IFileDialogService _fileDialogService;
    private readonly IEventAggregator _eventAggregator;
    private readonly INetworkValidationService _networkValidationService;
    
    private readonly LocationModel _locationModel;
    
    private string _controlIp;
    public string ControlIp
    {
        get => _controlIp;
        set => SetProperty(ref _controlIp, value);
    }
    
    private string _controlPort;
    public string ControlPort
    {
        get => _controlPort;
        set => SetProperty(ref _controlPort, value);
    }
    
    private string _stationIp;
    public string StationIp
    {
        get => _stationIp;
        set => SetProperty(ref _stationIp, value);
    }
    
    private string _stationPort;
    public string StationPort
    {
        get => _stationPort;
        set => SetProperty(ref _stationPort, value);
    }
    
    private ObservableCollection<Location> _locations;
    public ObservableCollection<Location> Locations 
    {
        get => _locations;
        set => SetProperty(ref _locations, value);
    }
    
    public DelegateCommand ControlApplyCommand { get; }
    public DelegateCommand StationApplyCommand { get; }
    public DelegateCommand SaveStationCoordinateSettingsCommand { get; }
    public DelegateCommand NavigateToGTrackNodeViewCommand { get; }
    
    public SettingViewModel(IRegionManager regionManager, IDialogService dialogService,
        IFileDialogService fileDialogService, IEventAggregator eventAggregator,
        INetworkValidationService networkValidationService)
    {
        _regionManager = regionManager;
        _dialogService = dialogService;
        _fileDialogService = fileDialogService;
        _eventAggregator = eventAggregator;
        _networkValidationService = networkValidationService;

        _locationModel = new LocationModel();

        Locations = _locationModel.Locations;

        ControlApplyCommand = new DelegateCommand(ControlApply);
        StationApplyCommand = new DelegateCommand(StationApply);
        SaveStationCoordinateSettingsCommand = new DelegateCommand(SaveStationCoordinateSettings);
        NavigateToGTrackNodeViewCommand = new DelegateCommand(Navigate);
    }

    private void ControlApply()
    {
        if (!_networkValidationService.IsValidIp(ControlIp) || !_networkValidationService.IsValidPort(ControlPort))
        {
            _dialogService.ShowDialog(nameof(MessageDialogView), new DialogParameters
            {
                { "message", "Invalid Node IP or Port. Please check your input." }
            }, r =>
            {
                _eventAggregator.GetEvent<AppMessageEvent>().Publish(
                    new ServerStatusMessage { ServerName = "Control", Status = "" });
            });
            return;
        }

        _dialogService.ShowDialog(nameof(MessageDialogView), new DialogParameters
        {
            { "message", "Control settings saved successfully!" }
        }, r =>
        {
            _eventAggregator.GetEvent<AppMessageEvent>().Publish(
                new ServerStatusMessage { ServerName = "Control", Status = "connection" });
        });
    }

    private void StationApply()
    {
        if (!_networkValidationService.IsValidIp(StationIp) || !_networkValidationService.IsValidPort(StationPort))
        {
            _dialogService.ShowDialog(nameof(MessageDialogView), new DialogParameters
            {
                { "message", "Invalid Node IP or Port. Please check your input." }
            }, r =>
            {
                _eventAggregator.GetEvent<AppMessageEvent>().Publish(
                    new ServerStatusMessage { ServerName = "Station", Status = "" });
            });
            return;
        }

        _dialogService.ShowDialog(nameof(MessageDialogView), new DialogParameters
        {
            { "message", "Station settings saved successfully!" }
        }, r =>
        {
            _eventAggregator.GetEvent<AppMessageEvent>().Publish(
                new ServerStatusMessage { ServerName = "Station", Status = "connection" });
        });
    }
    
    private void SaveStationCoordinateSettings()
    {
        _dialogService.ShowDialog(nameof(MessageDialogView), new DialogParameters
        {
            { "message", "Data saved!" }
        }, r =>
        { });
    }

    private void Navigate()
    {
        _regionManager.RequestNavigate("MainRegion", nameof(GTrackNodeView));
    }
    
    public void OnNavigatedTo(NavigationContext navigationContext) { }
    public bool IsNavigationTarget(NavigationContext navigationContext) => true;
    public void OnNavigatedFrom(NavigationContext navigationContext) { }
}