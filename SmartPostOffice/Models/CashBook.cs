using System;
using System.Collections.Generic;

namespace SmartPostOffice.Models;

public partial class CashBook
{
    public int EntryId { get; set; }

    public int? PaymentId { get; set; }

    public decimal? Amount { get; set; }

    public string? TransactionType { get; set; }

    public DateTime? EntryDate { get; set; }

    public virtual Payment? Payment { get; set; }
}
