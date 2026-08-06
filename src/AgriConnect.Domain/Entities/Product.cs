using AgriConnect.Domain.Common;

namespace AgriConnect.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int Quantity { get; set; }


    // Foreign Keys

    public int FarmerId { get; set; }

    public int CategoryId { get; set; }


    // Navigation Properties

    public Farmer Farmer { get; set; } = null!;

    public Category Category { get; set; } = null!;

    public ICollection<OrderItem> OrderItems { get; set; }
        = new List<OrderItem>();
}