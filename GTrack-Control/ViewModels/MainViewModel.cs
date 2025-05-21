using GTrack_Control.Services.Interfaces;
using GTrack_Control.Views;

namespace GTrack_Control.ViewModels;

public class MainViewModel : BindableBase
{
    private readonly IRegionManager _regionManager;
    private readonly IApplicationService _applicationService;

    private string _title;
    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public DelegateCommand NavigateToGTrackControlViewCommand { get; }
    public DelegateCommand NavigateToSettingViewCommand { get; }
    public DelegateCommand ApplicationExitCommand { get; }
    
    public MainViewModel(IRegionManager regionManager, IApplicationService applicationService)
    {
        _regionManager = regionManager;
        _applicationService = applicationService;

        Title = "GTrack-Control";
        
        NavigateToGTrackControlViewCommand = new DelegateCommand(NavigateToGTrackControlView);
        NavigateToSettingViewCommand = new DelegateCommand(NavigateToSettingView);
        ApplicationExitCommand = new DelegateCommand(ApplicationExit);
    }

    private void NavigateToGTrackControlView()
    {
        _regionManager.RequestNavigate("MainRegion", nameof(GTrackControlView));
    }
    
    private void NavigateToSettingView()
    {
        _regionManager.RequestNavigate("MainRegion", nameof(SettingView));
    }
    
    private void ApplicationExit()
    {
        _applicationService.Shutdown();
    }
}