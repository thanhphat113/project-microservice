using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityService.DTOs;
using IdentityService.Models;

namespace IdentityService.Services
{
    public interface IUserService
    {
        Task<dynamic> FindByIdAsync(int userId);

    }
    public class UserService : IUserService
    {
        private readonly IdentityDbContext _context;

        public UserService(IdentityDbContext context)
        {
            _context = context;
        }

        public async Task<dynamic> FindByIdAsync(int userId)
        {
            try
            {
                var currentUser = await _context.Users.FindAsync(userId);
                if (currentUser == null)
                    return ServiceResult<User>.Fail("Đăng nhập thất bại");
                return ServiceResult<User>.Ok(currentUser, "Đăng nhập thành công");

            }
            catch (System.Exception ex)
            {
                return ServiceResult<User>.Fail($"Đăng nhập thất bại, lỗi: {ex.Message}");
            }

        }
    }
}