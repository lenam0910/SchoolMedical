using System;
using System.Collections.Generic;

namespace SchoolMedicalWPF.Models;

public partial class MedicalRecord
{
    public int RecordId { get; set; }

    public int StudentId { get; set; }

    public string? Condition { get; set; }

    public string? Allergies { get; set; }

    public string? Notes { get; set; }

    public DateTime? RecordedDate { get; set; }

    public virtual Student Student { get; set; } = null!;
}
