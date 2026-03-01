using System;
using System.Collections.Generic;

namespace SmartPostOffice.Models;

public partial class BungalowBooking
{
    public int BookingId { get; set; }

    public int? CustomerId { get; set; }

    public string? BungalowName { get; set; }

    public DateOnly? CheckInDate { get; set; }

    public DateOnly? CheckOutDate { get; set; }

    public string? Status { get; set; }

    public virtual Customer? Customer { get; set; }
}
