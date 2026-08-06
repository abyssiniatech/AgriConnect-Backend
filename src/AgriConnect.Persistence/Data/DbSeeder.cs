using AgriConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgriConnect.Persistence.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(
        ApplicationDbContext context)
    {
        if (!await context.Roles.AnyAsync())
        {
            var roles = new List<Role>
            {
                new Role
                {
                    Name = "Admin",
                    Description = "System Administrator",
                    CreatedAt = DateTime.UtcNow
                },
                new Role
                {
                    Name = "Farmer",
                    Description = "Farm owner",
                    CreatedAt = DateTime.UtcNow
                }
            };

            await context.Roles.AddRangeAsync(roles);
            await context.SaveChangesAsync();
        }
    }
}