using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Threading.Tasks;
using IdentityService.DTOs;
using IdentityService.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Services
{
    public interface IRefreshTokenService
    {
        Task<ServiceResult<RefreshToken>> GenerateToken(int userId, string deviceId);
        Task<ServiceResult<RefreshToken>> RotateRefreshTokenAsync(string deviceId, string refreshToken);
        Task<ServiceResult<RefreshToken>> RevokeRefreshTokenAsync(string deviceId, string refreshToken);
        Task<int> DeleteExpiredOrRevokedTokensAsync();
    }
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IdentityDbContext _context;


        public RefreshTokenService(IdentityDbContext context)
        {
            _context = context;
        }

        public async Task<int> DeleteExpiredOrRevokedTokensAsync()
        {
            var tokens = await _context.RefreshTokens
                                .Where(r => r.ExpiryDate < DateTime.Now || r.IsRevoked)
                                .ToListAsync();

            _context.RefreshTokens.RemoveRange(tokens);

            var result = await _context.SaveChangesAsync();

            return result;
        }

        private async Task<RefreshToken> FindByDeviceIdAndRefreshToken(string deviceId, string refreshToken)
        {
            var targetToken = await _context.RefreshTokens
                                .FirstOrDefaultAsync(a => a.Token.Equals(refreshToken) &&
                                                        a.DeviceId.Equals(deviceId));

            return targetToken;
        }

        public async Task<ServiceResult<RefreshToken>> GenerateToken(int userId, string deviceId)
        {
            try
            {
                var token = Guid.NewGuid().ToString();
                var newRefreshToken = new RefreshToken
                {
                    DeviceId = deviceId,
                    UserId = userId,
                    Token = token,
                    ExpiryDate = DateTime.Now.AddDays(5)
                };

                await _context.RefreshTokens.AddAsync(newRefreshToken);
                var rowEffect = await _context.SaveChangesAsync();

                if (rowEffect > 0)
                {
                    return ServiceResult<RefreshToken>.Ok(newRefreshToken, "Tạo refresh token thành công");
                }

                return ServiceResult<RefreshToken>.Fail("Tạo refresh token thất bại");
            }
            catch (System.Exception ex)
            {
                return ServiceResult<RefreshToken>.Fail($"Tạo refresh token thất bại, lỗi: {ex.Message}");
            }

        }

        public async Task<ServiceResult<RefreshToken>> RevokeRefreshTokenAsync(string deviceId, string refreshToken)
        {
            try
            {
                var targetToken = await FindByDeviceIdAndRefreshToken(deviceId, refreshToken);
                if (targetToken == null) return ServiceResult<RefreshToken>.Fail("Refresh token không tồn tại !!!");

                targetToken.IsRevoked = true;

                await _context.SaveChangesAsync();

                return ServiceResult<RefreshToken>.Ok(targetToken, "Revoke thành công");
            }
            catch (System.Exception ex)
            {
                return ServiceResult<RefreshToken>.Fail($"Revoke thất bại, lỗi: {ex.Message}");
            }

        }

        public async Task<ServiceResult<RefreshToken>> RotateRefreshTokenAsync(string deviceId, string token)
        {
            try
            {
                var validateToken = await ValidateRefreshToken(deviceId, token);
                if (!validateToken) return ServiceResult<RefreshToken>.Fail("Refresh token hoặc thiết bị dùng refresh token không hợp lệ");

                var revokeToken = await RevokeRefreshTokenAsync(deviceId, token);
                if (!revokeToken.IsSuccess) return ServiceResult<RefreshToken>.Fail(revokeToken.Message);
                var revokedToken = revokeToken.Data;

                var generateToken = await GenerateToken(revokedToken.UserId, deviceId);
                if (!generateToken.IsSuccess) return ServiceResult<RefreshToken>.Fail(generateToken.Message);
                var newToken = generateToken.Data;

                return ServiceResult<RefreshToken>.Ok(newToken, "Tạo mới refresh thành công");
            }
            catch (System.Exception ex)
            {
                return ServiceResult<RefreshToken>.Fail($"Lỗi rotate token: {ex.Message}");
            }
        }

        private async Task<bool> ValidateRefreshToken(string deviceId, string refreshToken)
        {
            try
            {
                var targetToken = await _context.RefreshTokens
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(a => a.Token.Equals(refreshToken) &&
                                                            a.DeviceId.Equals(deviceId));

                if (targetToken == null ||
                    targetToken.ExpiryDate < DateTime.Now ||
                    targetToken.IsRevoked)
                    return false;

                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
    }
}