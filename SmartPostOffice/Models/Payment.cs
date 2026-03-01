using System;
using System.Collections.Generic;

namespace SmartPostOffice.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int RequestId { get; set; }

    public decimal Amount { get; set; }

    public string? PaymentType { get; set; }

    public DateTime? PaymentDate { get; set; }

    public virtual ICollection<CashBook> CashBooks { get; set; } = new List<CashBook>();

    public virtual ICollection<Receipt> Receipts { get; set; } = new List<Receipt>();

    public virtual ServiceRequest Request { get; set; } = null!;
}
