using AgriConnect.Domain.Common;

namespace AgriConnect.Domain.Entities;

public class WeatherData : BaseEntity
{
    public string Location { get; set; } = string.Empty;


    public decimal Temperature { get; set; }

    public decimal Rainfall { get; set; }

    public decimal Humidity { get; set; }


    public DateTime RecordedDate { get; set; }
}