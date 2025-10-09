using System;
using System.Collections.Generic;

namespace IdentityService.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string Provider { get; set; } = null!;

    public string Identify { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
