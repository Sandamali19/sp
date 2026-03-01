using System;
using System.Collections.Generic;

namespace SmartPostOffice.Models;

public partial class MailItem
{
    public int ItemId { get; set; }

    public int RequestId { get; set; }

    public string? ItemType { get; set; }

    public decimal? Weight { get; set; }

    public string? Destination { get; set; }

    public virtual ServiceRequest Request { get; set; } = null!;
}
