using AgriConnect.Domain.Common;

namespace AgriConnect.Domain.Entities;

public class User : BaseEntity
{
    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;


    // Foreign Key
    public int RoleId { get; set; }


    // Navigation Property
    public Role Role { get; set; } = null!;
}