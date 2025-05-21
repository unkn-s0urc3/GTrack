namespace GTrack_Control.Services.Interfaces;

public interface INetworkValidationService
{
    bool IsValidIp(string ip);
    
    bool IsValidPort(string port);
}