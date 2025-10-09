using IdentityService.DTOs;
using Microsoft.AspNetCore.Identity;
using IdentityService.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Services
{
    public interface IAuthService
    {
        public Task<dynamic> RegisterLocal(Account item);
        public Task<dynamic> LoginLocal(LoginLocalDTO item);
        public Task<dynamic> Logout(string deviceId, string refresh);

    }
    public class AuthService : IAuthService
    {
        private readonly IdentityDbContext _context;
        private readonly IRefreshTokenService _refresh;

        public AuthService(IdentityDbContext context, IRefreshTokenService refresh)
        {
            _context = context;
            _refresh = refresh;
        }

        public async Task<dynamic> RegisterLocal(Account item)
        {
            try
            {
                await _context.Accounts.AddAsync(item);
                var rowEffect = await _context.SaveChangesAsync();

                if (rowEffect > 0)
                {
                    return ServiceResult<bool>.Ok(true, "Đăng ký thành công");
                }

                return ServiceResult<bool>.Fail("Đăng ký thất bại");
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Fail($"Có lỗi xảy ra: {ex.Message}");
            }
        }

        public async Task<dynamic> LoginLocal(LoginLocalDTO item)
        {
            try
            {
                var hasher = new PasswordHasher<Account>();

                var currentAccount = await _context.Accounts
                    .FirstOrDefaultAsync(a => EF.Functions.Collate(a.Identify, "Latin1_General_CS_AS") == item.Identify);
                if (currentAccount == null)
                    return ServiceResult<User>.Fail($"Tên đăng nhập không hợp lệ");

                var verify = hasher.VerifyHashedPassword(null, currentAccount.PasswordHash, item.Password);
                if (verify == PasswordVerificationResult.Success)
                {
                    var currentUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == currentAccount.UserId);
                    return ServiceResult<User>.Ok(currentUser, "Đăng nhập thành công");
                }

                return ServiceResult<User>.Fail("Nhập sai mật khẩu");
            }
            catch (Exception ex)
            {
                return ServiceResult<User>.Fail($"Có lỗi xảy ra: {ex.Message}");
            }
        }


        public async Task<dynamic> Logout(string deviceId, string refresh)
        {
            var revokedToken = await _refresh.RevokeRefreshTokenAsync(deviceId, refresh);

            if (revokedToken == null) return ServiceResult<string>.Fail("Revoke token thất bại");

            return ServiceResult<string>.Ok("Revoke token thành công");
        }
    }
}