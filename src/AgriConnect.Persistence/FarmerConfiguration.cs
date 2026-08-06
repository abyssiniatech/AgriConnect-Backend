using AgriConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgriConnect.Persistence.Configurations;

public class FarmerConfiguration : IEntityTypeConfiguration<Farmer>
{
    public void Configure(EntityTypeBuilder<Farmer> builder)
    {
        builder.ToTable("Farmers");

        // Primary Key
        builder.HasKey(f => f.Id);


        // Properties
        builder.Property(f => f.FarmName)
            .IsRequired()
            .HasMaxLength(150);


        builder.Property(f => f.Location)
            .IsRequired()
            .HasMaxLength(200);


        builder.Property(f => f.FarmSize)
            .HasPrecision(10, 2);


        // Farmer -> User relationship
        builder.HasOne(f => f.User)
            .WithOne()
            .HasForeignKey<Farmer>(f => f.UserId)
            .OnDelete(DeleteBehavior.Cascade);


        // Farmer -> Farms relationship
        builder.HasMany(f => f.Farms)
            .WithOne(f => f.Farmer)
            .HasForeignKey(f => f.FarmerId)
            .OnDelete(DeleteBehavior.Cascade);


        // One user can only have one farmer profile
        builder.HasIndex(f => f.UserId)
            .IsUnique();
    }
}