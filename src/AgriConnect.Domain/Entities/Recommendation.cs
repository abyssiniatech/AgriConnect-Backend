using AgriConnect.Domain.Common;

namespace AgriConnect.Domain.Entities;

public class Recommendation : BaseEntity
{
    public int AgriculturalExpertId { get; set; }

    public int FarmerId { get; set; }


    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;


    public DateTime CreatedDate { get; set; }


    // Navigation Property

    public AgriculturalExpert AgriculturalExpert { get; set; } = null!;
}