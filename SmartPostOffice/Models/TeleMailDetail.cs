using System;
using System.Collections.Generic;

namespace SmartPostOffice.Models;

public partial class TeleMailDetail
{
    public int TeleMailId { get; set; }

    public int RequestId { get; set; }

    public string? MessageContent { get; set; }

    public string? ReceiverName { get; set; }

    public string? ReceiverAddress { get; set; }

    public virtual ServiceRequest Request { get; set; } = null!;
}
