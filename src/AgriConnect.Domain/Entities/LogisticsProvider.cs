using AgriConnect.Domain.Common;

namespace AgriConnect.Domain.Entities;

public class LogisticsProvider : BaseEntity
{
    public string CompanyName { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;


    // Navigation Property

    public ICollection<Vehicle> Vehicles { get; set; }
        = new List<Vehicle>();

    public ICollection<Delivery> Deliveries { get; set; }
        = new List<Delivery>();
}