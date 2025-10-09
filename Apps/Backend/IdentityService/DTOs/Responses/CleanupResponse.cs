using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.DTOs.Responses
{
    public class CleanupResponse
    {
        public DateTime Time { get; } = DateTime.Now;
        public int Quantity { get; set; } = 0;
    }
}