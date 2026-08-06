using AgriConnect.Domain.Common;

namespace AgriConnect.Domain.Entities;

public class Farmer : BaseEntity
{
    public int UserId { get; set; }

    public string FarmName { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public decimal FarmSize { get; set; }


    // Navigation Property
    public User User { get; set; } = null!;

    public ICollection<Farm> Farms { get; set; } = new List<Farm>();
}