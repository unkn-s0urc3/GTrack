using GTrack_Services.Interfaces;

namespace GTrack_Services;

public class ApplicationService : IApplicationService
{
    public void Shutdown() { System.Windows.Application.Current.Shutdown(); }
}