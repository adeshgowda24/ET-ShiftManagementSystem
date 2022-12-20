using System;
using System.Collections.Generic;

namespace testet.Models;

public partial class Shift
{
    public int ShiftId { get; set; }

    public string ShiftName { get; set; } = null!;

    public DateTime Timing { get; set; }

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual ICollection<ShiftControl> ShiftControls { get; } = new List<ShiftControl>();
}
