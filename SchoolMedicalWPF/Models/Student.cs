using System;
using System.Collections.Generic;

namespace SchoolMedicalWPF.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string FullName => $"{LastName} {FirstName}";

    public DateOnly DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? Class { get; set; }

    public string? EmergencyContact { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual ICollection<HealthIncident> HealthIncidents { get; set; } = new List<HealthIncident>();

    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    public virtual ICollection<Medication> Medications { get; set; } = new List<Medication>();
}
