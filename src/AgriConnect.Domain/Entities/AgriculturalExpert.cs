using AgriConnect.Domain.Common;

namespace AgriConnect.Domain.Entities;

public class AgriculturalExpert : BaseEntity
{
    public string FullName { get; set; } = string.Empty;

    public string Specialization { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;


    // Navigation Properties

    public ICollection<Consultation> Consultations { get; set; }
        = new List<Consultation>();

    public ICollection<Recommendation> Recommendations { get; set; }
        = new List<Recommendation>();
}