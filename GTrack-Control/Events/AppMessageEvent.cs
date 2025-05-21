namespace GTrack_Control.Events;

public class ServerStatusMessage
{
    public string ServerName { get; set; }
    
    public string Status { get; set; }
}

public class AppMessageEvent : PubSubEvent<ServerStatusMessage> { }