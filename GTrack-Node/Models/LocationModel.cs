using System.Collections.ObjectModel;

namespace GTrack_Node.Models;

public class Location
{
    public string Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Height { get; set; }
}

public class LocationModel
{
    public ObservableCollection<Location> Locations { get; }
    
    public LocationModel()
    {
        Locations = new ObservableCollection<Location>
        {
            new Location { Name = "Красноярск", Latitude = 55.75, Longitude = 37.61, Height = 156 },
        };
    }
}