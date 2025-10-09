using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.DTOs
{
    public class ServiceResult<T>
    {
        public bool IsSuccess { get; set; } = false;
        public string? Message { get; set; }
        public T? Data { get; set; }

        public static ServiceResult<T> Ok(T data, string? message = null)
            => new ServiceResult<T> { IsSuccess = true, Data = data, Message = message };

        public static ServiceResult<T> Fail(string message)
            => new ServiceResult<T> { IsSuccess = false, Message = message };

    }
}