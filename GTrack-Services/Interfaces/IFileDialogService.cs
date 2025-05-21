namespace GTrack_Services.Interfaces;

public interface IFileDialogService
{
    string OpenFile(string filter = "All files (*.*)|*.*");
    
    string SaveFile(string filter = "All files (*.*)|*.*");
}