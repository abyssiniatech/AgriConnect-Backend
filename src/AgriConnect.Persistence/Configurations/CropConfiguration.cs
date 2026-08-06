using AgriConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgriConnect.Persistence.Configurations;

public class CropConfiguration : IEntityTypeConfiguration<Crop>
{
    public void Configure(EntityTypeBuilder<Crop> builder)
    {
        builder.ToTable("Crops");

        // Primary Key
        builder.HasKey(c => c.Id);

        // Properties
        builder.Property(c => c.CropName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.PlantingDate)
            .IsRequired();

        builder.Property(c => c.HarvestDate);

        // Relationship: Crop -> Farm
        builder.HasOne(c => c.Farm)
            .WithMany(f => f.Crops)
            .HasForeignKey(c => c.FarmId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}