using System.Collections.ObjectModel;

namespace GTrack_Node.Models;

public class ObservationParameter
{
    public string Name { get; set; }
    public string Value { get; set; }
}

public class ObservationParameterModel
{
    public ObservableCollection<ObservationParameter> Parameters { get; }

    public ObservationParameterModel()
    {
        Parameters = new ObservableCollection<ObservationParameter>
        {
            new ObservationParameter { Name = "Станция", Value = "МСК-1" },
            new ObservationParameter { Name = "Спутник", Value = "Союз" },
            new ObservationParameter { Name = "Следующий сеанс", Value = "12:45" },
            new ObservationParameter { Name = "Azimuth", Value = "135.2°" },
            new ObservationParameter { Name = "Elevation", Value = "45.8°" },
            new ObservationParameter { Name = "Slant Range", Value = "1200 км" },
            new ObservationParameter { Name = "Altitude", Value = "400 км" },
            new ObservationParameter { Name = "Doppler", Value = "+1200 Гц" }
        };
    }
}