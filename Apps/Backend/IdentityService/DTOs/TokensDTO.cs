using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.DTOs
{
    public class TokensDTO
    {
        public string Access { get; set; } = null!;
        public string Refresh { get; set; } = null!;
    }
}