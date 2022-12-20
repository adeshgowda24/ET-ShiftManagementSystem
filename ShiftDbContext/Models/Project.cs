using System;
using System.Collections.Generic;

namespace testet.Models;

public partial class Project
{
    public int ProjectId { get; set; }

    public string ProjectName { get; set; } = null!;

    public string? Description { get; set; }

    public string? ClientName { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<ShiftControl> ShiftControls { get; } = new List<ShiftControl>();
}
