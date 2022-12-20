using System;
using System.Collections.Generic;

namespace testet.Models;

public partial class ShiftControl
{
    public int Id { get; set; }

    public int ProjectId { get; set; }

    public int ShiftId { get; set; }

    public int UserId { get; set; }

    public bool IsActive { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual Shift Shift { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
