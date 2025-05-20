namespace GTrack_Control.ViewModels;

public class MessageDialogViewModel : BindableBase, IDialogAware
{
    private string _message;
    public string Message
    {
        get => _message;
        set => SetProperty(ref _message, value);
    }
    
    public DelegateCommand CloseDialogCommand { get; }
    
    public MessageDialogViewModel()
    {
        CloseDialogCommand = new DelegateCommand(() => RequestClose.Invoke(new DialogResult(ButtonResult.OK)));
    }
    
    public bool CanCloseDialog() => true;
    
    public void OnDialogClosed() { }
 
    public void OnDialogOpened(IDialogParameters parameters)
    {
        Message = parameters.GetValue<string>("message");
    }

    public DialogCloseListener RequestClose { get; }
}