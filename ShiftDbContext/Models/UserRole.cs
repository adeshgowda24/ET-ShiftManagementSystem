using System;
using System.Collections.Generic;

namespace testet.Models;

public partial class UserRole
{
    public int RoleId { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
