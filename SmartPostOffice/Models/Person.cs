using System;
using System.Collections.Generic;

namespace SmartPostOffice.Models;

public partial class Person
{
    public int PersonId { get; set; }

    public string FullName { get; set; } = null!;

    public string? Address { get; set; }

    public string ContactNumber { get; set; } = null!;

    public virtual Customer? Customer { get; set; }

    public virtual PostOfficer? PostOfficer { get; set; }
}
