using AgriConnect.Domain.Common;

namespace AgriConnect.Domain.Entities;

public class Farm : BaseEntity
{
    public int FarmerId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public decimal Size { get; set; }


    // Navigation Property
    public Farmer Farmer { get; set; } = null!;

    public ICollection<Crop> Crops { get; set; } = new List<Crop>();
}