using System.Collections.ObjectModel;
using GTrack_Control.Views;
using GTrack_Events;
using GTrack_Events.Messages;
using GTrack_MessageDialogModule.Views;
using GTrack_Services.Interfaces;

namespace GTrack_Control.ViewModels;

public class SettingViewModel : BindableBase, INavigationAware
{
    private readonly IRegionManager _regionManager;
    private readonly IDialogService _dialogService;
    private readonly IFileDialogService _fileDialogService;
    private readonly IEventAggregator _eventAggregator;
    private readonly INetworkValidationService _networkValidationService;

    private string _nodeIpAddress;
    public string NodeIpAddress
    {
        get => _nodeIpAddress;
        set => SetProperty(ref _nodeIpAddress, value);
    }

    private string _nodePort;
    public string NodePort
    {
        get => _nodePort;
        set => SetProperty(ref _nodePort, value);
    }
    
    private string _houstonIpAddress;
    public string HoustonIpAddress
    {
        get => _houstonIpAddress;
        set => SetProperty(ref _houstonIpAddress, value);
    }

    private string _houstonPort;
    public string HoustonPort
    {
        get => _houstonPort;
        set => SetProperty(ref _houstonPort, value);
    }
    
    private string _tleFilePath;
    public string TleFilePath
    {
        get => _tleFilePath;
        set => SetProperty(ref _tleFilePath, value);
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
    
    public DelegateCommand LoadTleCommand { get; }
    public DelegateCommand SaveHoustonSettingsCommand { get; }
    public DelegateCommand SaveNodeSettingsCommand { get; }
    public DelegateCommand NavigateToGTrackControlViewCommand { get; }

    public SettingViewModel(IRegionManager regionManager, IDialogService dialogService,
        IFileDialogService fileDialogService, IEventAggregator eventAggregator,
        INetworkValidationService networkValidationService)
    {
        _regionManager = regionManager;
        _dialogService = dialogService;
        _fileDialogService = fileDialogService;
        _eventAggregator = eventAggregator;
        _networkValidationService = networkValidationService;
        
        SaveNodeSettingsCommand = new DelegateCommand(OnSaveNodeSettings);
        SaveHoustonSettingsCommand = new DelegateCommand(OnSaveHoustonSettings);
        LoadTleCommand = new DelegateCommand(OnLoadTle);
        NavigateToGTrackControlViewCommand = new DelegateCommand(Navigate);
        
        // variables
        GTrackStations = new ObservableCollection<string>
        {
            "Station 1", "Station 2", "Station 3"
        };
    }

    private void OnSaveNodeSettings()
    {
        if (!_networkValidationService.IsValidIp(NodeIpAddress) || !_networkValidationService.IsValidPort(NodePort))
        {
            _dialogService.ShowDialog(nameof(MessageDialogView), new DialogParameters
            {
                { "message", "Invalid Node IP or Port. Please check your input." }
            }, r =>
            {
                _eventAggregator.GetEvent<AppMessageEvent>().Publish(
                    new ServerStatusMessage { ServerName = "Node", Status = "" });
            });
            return;
        }

        _dialogService.ShowDialog(nameof(MessageDialogView), new DialogParameters
        {
            { "message", "Node settings saved successfully!" }
        }, r =>
        {
            _eventAggregator.GetEvent<AppMessageEvent>().Publish(
                new ServerStatusMessage { ServerName = "Node", Status = "connection" });
        });
    }

    private void OnSaveHoustonSettings()
    {
        if (!_networkValidationService.IsValidIp(HoustonIpAddress) || !_networkValidationService.IsValidPort(HoustonPort))
        {
            _dialogService.ShowDialog(nameof(MessageDialogView), new DialogParameters
            {
                { "message", "Invalid Houston IP or Port. Please check your input." }
            }, r =>
            {
                _eventAggregator.GetEvent<AppMessageEvent>().Publish(
                    new ServerStatusMessage { ServerName = "Houston", Status = "" });
            });
            return;
        }

        _dialogService.ShowDialog(nameof(MessageDialogView), new DialogParameters
        {
            { "message", "Houston settings saved successfully!" }
        }, r =>
        {
            _eventAggregator.GetEvent<AppMessageEvent>().Publish(
                new ServerStatusMessage { ServerName = "Houston", Status = "connection" });
        });
    }

    private void OnLoadTle()
    {
        var filePath = _fileDialogService.OpenFile("TLE files (*.txt)|*.txt|All files (*.*)|*.*");

        if (!string.IsNullOrEmpty(filePath))
        {
            TleFilePath = filePath;

            _dialogService.ShowDialog(nameof(MessageDialogView), new DialogParameters
            {
                { "message", $"TLE file loaded:\n{filePath}" }
            }, r => { });
        }
        else
        {
            TleFilePath = string.Empty;

            _dialogService.ShowDialog(nameof(MessageDialogView), new DialogParameters
            {
                { "message", "No file selected." }
            }, r => { });
        }
    }
    
    private void Navigate()
    {
        _regionManager.RequestNavigate("MainRegion", nameof(GTrackControlView));
    }
    
    public void OnNavigatedTo(NavigationContext navigationContext) { }
    public bool IsNavigationTarget(NavigationContext navigationContext) => true;
    public void OnNavigatedFrom(NavigationContext navigationContext) { }
}