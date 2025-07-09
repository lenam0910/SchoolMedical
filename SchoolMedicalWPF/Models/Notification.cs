using System;
using System.Collections.Generic;

namespace SchoolMedicalWPF.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public int StaffId { get; set; }

    public string Message { get; set; } = null!;

    public bool? IsRead { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Staff Staff { get; set; } = null!;
}
