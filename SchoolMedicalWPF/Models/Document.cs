using System;
using System.Collections.Generic;

namespace SchoolMedicalWPF.Models;

public partial class Document
{
    public int DocumentId { get; set; }

    public int StudentId { get; set; }

    public string FileName { get; set; } = null!;

    public string FilePath { get; set; } = null!;

    public string? Description { get; set; }

    public int? UploadedBy { get; set; }

    public DateTime? UploadedAt { get; set; }

    public virtual Student Student { get; set; } = null!;

    public virtual Staff? UploadedByNavigation { get; set; }
}
