﻿using System;
using System.Collections.Generic;

namespace IdentityService.Models;

public partial class RefreshToken
{
    public int TokenId { get; set; }

    public string Token { get; set; } = null!;

    public DateTime ExpiryDate { get; set; }

    public string DeviceId { get; set; } = null!;

    public bool IsRevoked { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
