using System;
using System.Collections.Generic;

namespace testet.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public string Comments { get; set; } = null!;

    public int ShiftId { get; set; }

    public int UserId { get; set; }

    public bool Shared { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual Shift Shift { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
