using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.DTOs.Requests
{
    public class RefreshTokenRequest
    {
        public string DeviceId { get; set; }
    }
}