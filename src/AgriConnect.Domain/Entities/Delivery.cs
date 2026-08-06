using AgriConnect.Domain.Common;

namespace AgriConnect.Domain.Entities;

public class Delivery : BaseEntity
{
    public int OrderId { get; set; }

    public int LogisticsProviderId { get; set; }

    public int VehicleId { get; set; }


    public DateTime DeliveryDate { get; set; }

    public string Status { get; set; } = string.Empty;


    // Navigation Properties

    public Order Order { get; set; } = null!;

    public LogisticsProvider LogisticsProvider { get; set; } = null!;

    public Vehicle Vehicle { get; set; } = null!;


    public Tracking? Tracking { get; set; }
}