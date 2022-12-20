using System;
using System.Collections.Generic;

namespace testet.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public string Gender { get; set; } = null!;

    public bool IsVerified { get; set; }

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual ICollection<ShiftControl> ShiftControls { get; } = new List<ShiftControl>();

    public virtual ICollection<UserRole> UserRoles { get; } = new List<UserRole>();
}
