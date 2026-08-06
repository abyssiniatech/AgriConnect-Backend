using AgriConnect.Domain.Common;

namespace AgriConnect.Domain.Entities;

public class Crop : BaseEntity
{
    public int FarmId { get; set; }

    public string CropName { get; set; } = string.Empty;

    public DateTime PlantingDate { get; set; }

    public DateTime? HarvestDate { get; set; }


    // Navigation Property
    public Farm Farm { get; set; } = null!;
}