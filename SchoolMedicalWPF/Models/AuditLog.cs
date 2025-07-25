﻿using System;
using System.Collections.Generic;

namespace SchoolMedicalWPF.Models;

public partial class AuditLog
{
    public int LogId { get; set; }

    public int StaffId { get; set; }

    public string Action { get; set; } = null!;

    public string? Details { get; set; }

    public DateTime? LogDate { get; set; }

    public virtual Staff Staff { get; set; } = null!;
}
