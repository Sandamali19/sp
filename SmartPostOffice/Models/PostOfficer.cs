using System;
using System.Collections.Generic;

namespace SmartPostOffice.Models;

public partial class PostOfficer
{
    public int OfficerId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual Person Officer { get; set; } = null!;

    public virtual ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();
}
