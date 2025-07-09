using System;
using System.Collections.Generic;

namespace SchoolMedicalWPF.Models;

public partial class Medication
{
    public int MedicationId { get; set; }

    public int StudentId { get; set; }

    public string Name { get; set; } = null!;

    public string? Dosage { get; set; }

    public string? Frequency { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public int? PrescribedBy { get; set; }

    public virtual Staff? PrescribedByNavigation { get; set; }

    public virtual Student Student { get; set; } = null!;
}
