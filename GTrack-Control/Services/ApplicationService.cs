using GTrack_Control.Services.Interfaces;

namespace GTrack_Control.Services;

public class ApplicationService : IApplicationService
{
    public void Shutdown() { System.Windows.Application.Current.Shutdown(); }
}