using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.DTOs
{
    public class RegisterLocalDTO
    {
        public string Name { get; set; }
        public string Identify { get; set; }

        public string Password { get; set; }
    }
}