using GTrack_Node.Views;
using GTrack_Services.Interfaces;

namespace GTrack_Node.ViewModels;

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
    
    public DelegateCommand NavigateToGTrackNodeViewCommand { get; }
    public DelegateCommand NavigateToSettingViewCommand { get; }
    public DelegateCommand ApplicationExitCommand { get; }
    
    public MainViewModel(IRegionManager regionManager, IApplicationService applicationService)
    {
        _regionManager = regionManager;
        _applicationService = applicationService;

        Title = "GTrack-Node";
        
        NavigateToGTrackNodeViewCommand = new DelegateCommand(NavigateToGTrackNodeView);
        NavigateToSettingViewCommand = new DelegateCommand(NavigateToSettingView);
        ApplicationExitCommand = new DelegateCommand(ApplicationExit);
    }

    private void NavigateToGTrackNodeView()
    {
        _regionManager.RequestNavigate("MainRegion", nameof(GTrackNodeView));
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