using System;
using System.Collections.Generic;

namespace IdentityService.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}
