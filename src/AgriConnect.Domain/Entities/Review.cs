using AgriConnect.Domain.Common;

namespace AgriConnect.Domain.Entities;

public class Review : BaseEntity
{
    public int ProductId { get; set; }

    public int BuyerId { get; set; }


    public int Rating { get; set; }

    public string Comment { get; set; } = string.Empty;


    public Product Product { get; set; } = null!;
}