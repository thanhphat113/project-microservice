using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.DTOs
{
    public class ControllerResponse<T>
    {
        public string? Message { get; set; }
        public T? Data { get; set; }

        public static ControllerResponse<T> Ok(T data, string? message = null)
            => new ControllerResponse<T> { Data = data, Message = message };

        public static ControllerResponse<T> Fail(string message)
            => new ControllerResponse<T> { Message = message };
    }
}