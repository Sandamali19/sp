using System;
using System.Collections.Generic;

namespace SmartPostOffice.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<BungalowBooking> BungalowBookings { get; set; } = new List<BungalowBooking>();

    public virtual Person CustomerNavigation { get; set; } = null!;

    public virtual ICollection<StampOrder> StampOrders { get; set; } = new List<StampOrder>();
}
