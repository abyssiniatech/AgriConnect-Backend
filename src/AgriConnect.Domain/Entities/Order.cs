using AgriConnect.Domain.Common;

namespace AgriConnect.Domain.Entities;

public class Order : BaseEntity
{
    public int BuyerId { get; set; }

    public DateTime OrderDate { get; set; }


    public ICollection<OrderItem> OrderItems { get; set; }
        = new List<OrderItem>();


    public Payment? Payment { get; set; }
}