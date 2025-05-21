using System.Net;
using GTrack_Services.Interfaces;

namespace GTrack_Services;

public class NetworkValidationService : INetworkValidationService
{
    public bool IsValidIp(string ip)
    {
        if (IPAddress.TryParse(ip, out IPAddress ipAddr) &&
            ipAddr.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
        {
            string[] segments = ip.Split('.');
            if (segments.Length != 4)
                return false;

            foreach (var segment in segments)
            {
                if (!int.TryParse(segment, out int number) || number < 0 || number > 255)
                    return false;
            }

            return true;
        }

        return false;
    }

    public bool IsValidPort(string port)
    {
        return int.TryParse(port, out int portNumber) &&
               portNumber >= 1024 &&
               portNumber <= 65535;
    }
}