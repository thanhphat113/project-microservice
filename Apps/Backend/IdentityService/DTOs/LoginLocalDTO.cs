using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.DTOs
{
    public class LoginLocalDTO
    {
        public string Identify { get; set; }

        public string Password { get; set; }
        public string DeviceId { get; set; } = Guid.NewGuid().ToString();
    }
}