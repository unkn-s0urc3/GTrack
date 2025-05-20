using GTrack_Control.Events;
using GTrack_Control.Services.Interfaces;
using GTrack_Control.Views;

namespace GTrack_Control.ViewModels;

public class SettingViewModel : BindableBase, INavigationAware
{
    private readonly IRegionManager _regionManager;
    private readonly IEventAggregator _eventAggregator;
    private readonly IDialogService _dialogService;
    private readonly IFileDialogService _fileDialogService;

    public DelegateCommand NavigateToGTrackControlViewCommand { get; }
    public DelegateCommand SendMessageEventCommand { get; }

    public SettingViewModel(IRegionManager regionManager, 
        IEventAggregator eventAggregator, IDialogService dialogService,
        IFileDialogService fileDialogService)
    {
        _regionManager = regionManager;
        _eventAggregator = eventAggregator;
        _dialogService = dialogService;
        _fileDialogService = fileDialogService;
        
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
        
        var parameters = new DialogParameters { { "message", "Hello from Setting!" } };
        _dialogService.ShowDialog(nameof(MessageDialogView), parameters, r =>
        {
            if (r.Result == ButtonResult.OK)
            {
                OpenFile();
            }
        });
    }

    private void OpenFile()
    {
        var filePath = _fileDialogService.OpenFile("Text files (*.txt)|*.txt|All files (*.*)|*.*");

        if (!string.IsNullOrEmpty(filePath))
        {
            _dialogService.ShowDialog("MessageDialog", new DialogParameters
            {
                { "message", $"File selected: {filePath}" }
            }, r => { });
        }
    }
    
    public void OnNavigatedTo(NavigationContext navigationContext) { }
    public bool IsNavigationTarget(NavigationContext navigationContext) => true;
    public void OnNavigatedFrom(NavigationContext navigationContext) { }
}