using AgriConnect.Domain.Common;

namespace AgriConnect.Domain.Entities;

public class Vehicle : BaseEntity
{
    public int LogisticsProviderId { get; set; }

    public string VehicleNumber { get; set; } = string.Empty;

    public string VehicleType { get; set; } = string.Empty;


    // Navigation Property

    public LogisticsProvider LogisticsProvider { get; set; } = null!;
}