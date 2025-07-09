using System;
using System.Collections.Generic;

namespace SchoolMedicalWPF.Models;

public partial class Staff
{
    public int StaffId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Contact { get; set; }

    public DateTime? LastLogin { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual ICollection<HealthIncident> HealthIncidents { get; set; } = new List<HealthIncident>();

    public virtual ICollection<Medication> Medications { get; set; } = new List<Medication>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
