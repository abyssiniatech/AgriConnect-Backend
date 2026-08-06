using AgriConnect.Domain.Common;

namespace AgriConnect.Domain.Entities;

public class Payment : BaseEntity
{
    public int OrderId { get; set; }

    public decimal Amount { get; set; }

    public string PaymentMethod { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;


    public Order Order { get; set; } = null!;
}