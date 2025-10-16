using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.DTOs
{
    public class LoginUserDTO
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }

    }
}