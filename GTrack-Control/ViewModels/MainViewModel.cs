using GTrack_Control.Events;

namespace GTrack_Control.ViewModels;

public class MainViewModel : BindableBase
{
    private readonly IEventAggregator _eventAggregator;

    private string _title;
    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public MainViewModel(IEventAggregator eventAggregator)
    {
        _eventAggregator = eventAggregator;

        Title = "GTrack-Control";
        
        _eventAggregator.GetEvent<AppMessageEvent>().Subscribe(OnMessageReceived);
    }

    private void OnMessageReceived(string message)
    {
        Title = $"Message: {message}";
    }
}