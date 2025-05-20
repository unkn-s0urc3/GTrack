using GTrack_Control.Services.Interfaces;
using Microsoft.Win32;

namespace GTrack_Control.Services;

public class FileDialogService : IFileDialogService
{
    public string OpenFile(string filter = "All files (*.*)|*.*")
    {
        var dialog = new OpenFileDialog
        {
            Filter = filter
        };

        return dialog.ShowDialog() == true ? dialog.FileName : null;
    }

    public string SaveFile(string filter = "All files (*.*)|*.*")
    {
        var dialog = new SaveFileDialog
        {
            Filter = filter
        };

        return dialog.ShowDialog() == true ? dialog.FileName : null;
    }
}