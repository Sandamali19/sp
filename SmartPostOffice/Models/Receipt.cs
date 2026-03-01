using System;
using System.Collections.Generic;

namespace SmartPostOffice.Models;

public partial class Receipt
{
    public int ReceiptId { get; set; }

    public int PaymentId { get; set; }

    public string? ReceiptNumber { get; set; }

    public DateTime? GeneratedDate { get; set; }

    public virtual Payment Payment { get; set; } = null!;
}
