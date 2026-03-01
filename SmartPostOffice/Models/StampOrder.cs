using System;
using System.Collections.Generic;

namespace SmartPostOffice.Models;

public partial class StampOrder
{
    public int OrderId { get; set; }

    public int? CustomerId { get; set; }

    public string? DesignFilePath { get; set; }

    public int? Quantity { get; set; }

    public DateTime? OrderDate { get; set; }

    public virtual Customer? Customer { get; set; }
}
