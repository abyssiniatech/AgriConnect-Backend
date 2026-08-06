using AgriConnect.Domain.Common;

namespace AgriConnect.Domain.Entities;

public class Tracking : BaseEntity
{
    public int DeliveryId { get; set; }


    public string CurrentLocation { get; set; } = string.Empty;

    // Navigation Property

    public Delivery Delivery { get; set; } = null!;
}