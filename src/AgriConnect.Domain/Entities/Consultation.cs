using AgriConnect.Domain.Common;

namespace AgriConnect.Domain.Entities;

public class Consultation : BaseEntity
{
    public int FarmerId { get; set; }

    public int AgriculturalExpertId { get; set; }


    public string Question { get; set; } = string.Empty;

    public string Answer { get; set; } = string.Empty;


    public DateTime ConsultationDate { get; set; }


    // Navigation Properties

    public Farmer Farmer { get; set; } = null!;

    public AgriculturalExpert AgriculturalExpert { get; set; } = null!;
}