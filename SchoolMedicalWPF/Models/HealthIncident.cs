using System;
using System.Collections.Generic;

namespace SchoolMedicalWPF.Models;

public partial class HealthIncident
{
    public int IncidentId { get; set; }

    public int StudentId { get; set; }

    public int StaffId { get; set; }

    public DateTime IncidentDate { get; set; }

    public string? Description { get; set; }

    public string? ActionTaken { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Staff Staff { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
