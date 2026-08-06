using AgriConnect.Domain.Common;

namespace AgriConnect.Domain.Entities;

public class AuditLog : BaseEntity
{
    public int UserId { get; set; }

    public string Action { get; set; } = string.Empty;

    public string EntityName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;


    public DateTime CreatedDate { get; set; }


    public User User { get; set; } = null!;
}