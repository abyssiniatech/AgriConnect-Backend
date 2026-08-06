using AgriConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgriConnect.Persistence.Configurations;

public class FarmConfiguration : IEntityTypeConfiguration<Farm>
{
    public void Configure(EntityTypeBuilder<Farm> builder)
    {
        builder.ToTable("Farms");


        // Primary Key
        builder.HasKey(f => f.Id);


        // Properties
        builder.Property(f => f.Name)
            .IsRequired()
            .HasMaxLength(150);


        builder.Property(f => f.Location)
            .IsRequired()
            .HasMaxLength(200);


        builder.Property(f => f.Size)
            .HasPrecision(10, 2);



        // Farm -> Farmer relationship
        builder.HasOne(f => f.Farmer)
            .WithMany(f => f.Farms)
            .HasForeignKey(f => f.FarmerId)
            .OnDelete(DeleteBehavior.Cascade);



        // Farm -> Crops relationship
        builder.HasMany(f => f.Crops)
            .WithOne(c => c.Farm)
            .HasForeignKey(c => c.FarmId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}