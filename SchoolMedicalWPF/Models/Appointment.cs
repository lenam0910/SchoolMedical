using System;
using System.Collections.Generic;

namespace SchoolMedicalWPF.Models;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public int StudentId { get; set; }

    public int StaffId { get; set; }

    public DateTime AppointmentDate { get; set; }

    public string? Reason { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Staff Staff { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
