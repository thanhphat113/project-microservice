using System.Diagnostics;
using AutoMapper;
using IdentityService.Authentication;
using IdentityService.DTOs;
using IdentityService.DTOs.Responses;
using IdentityService.Models;
using IdentityService.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        private readonly JwtToken _jwt;

        private readonly IMapper _mapper;
        private readonly IRefreshTokenService _refresh;
        private readonly IConfiguration _configuration;
        private readonly IUserService _user;
        private readonly IWebHostEnvironment _env;


        public AuthController(IConfiguration configuration, JwtToken jwt, IAuthService auth, IMapper mapper, IRefreshTokenService refresh, IUserService user, IWebHostEnvironment env)
        {
            _auth = auth;
            _jwt = jwt;
            _mapper = mapper;
            _refresh = refresh;
            _configuration = configuration;
            _user = user;
            _env = env;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody] RegisterLocalDTO request)
        {
            var sw = Stopwatch.StartNew();
            var hasher = new PasswordHasher<Account>();
            var hashedPassword = hasher.HashPassword(null, request.Password);

            var newAccount = _mapper.Map<Account>(request);
            newAccount.PasswordHash = hashedPassword;

            var register = await _auth.RegisterLocal(newAccount);

            sw.Stop();
            Console.WriteLine($"Register API took {sw.ElapsedMilliseconds} ms");

            if (register.IsSuccess) return Ok(ControllerResponse<bool>.Ok(register.Data, register.Message));

            return BadRequest(ControllerResponse<bool>.Fail(register.Message));
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginLocalAsync([FromBody] LoginLocalDTO data)
        {
            var result = await _auth.LoginLocal(data);
            var message = result.Message;

            if (!result.IsSuccess || result.Data == null)
            {
                return BadRequest(ControllerResponse<LoginUserDTO>.Fail(result.Message));
            }

            var accessToken = _jwt.GenerateJwtToken(result.Data.UserId);
            var generateRefreshToken = await _refresh.GenerateToken(result.Data.UserId, data.DeviceId);
            if (!generateRefreshToken.IsSuccess) return BadRequest(ControllerResponse<LoginUserDTO>.Fail(generateRefreshToken.Message));
            var refreshToken = generateRefreshToken.Data;

            CreateCookieForTokens(accessToken, refreshToken.Token, data.DeviceId);

            var dataMapper = _mapper.Map<LoginUserDTO>(result.Data);
            var response = ControllerResponse<LoginUserDTO>.Ok(dataMapper, message);
            return Ok(response);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken()
        {
            if (HttpContext.Request.Cookies.TryGetValue("refresh", out var refresh) && HttpContext.Request.Cookies.TryGetValue("deviceId", out var deviceId))
            {

                var rotateToken = await _refresh.RotateRefreshTokenAsync(deviceId, refresh);
                if (!rotateToken.IsSuccess) return BadRequest(ControllerResponse<LoginUserDTO>.Fail(rotateToken.Message));
                var newToken = rotateToken.Data;

                var newAccessToken = _jwt.GenerateJwtToken(newToken.UserId);

                CreateCookieForTokens(newAccessToken, newToken.Token, newToken.DeviceId);

                var userResponse = await _user.FindByIdAsync(newToken.UserId);
                if (!userResponse.IsSuccess) return BadRequest(ControllerResponse<LoginUserDTO>.Fail(userResponse.Message));

                var userMapper = _mapper.Map<LoginUserDTO>(userResponse.Data);
                return Ok(ControllerResponse<LoginUserDTO>.Ok(userMapper, "Cấp access thành công"));
            }

            return BadRequest(ControllerResponse<LoginUserDTO>.Fail("Không có refresh token nào cả"));
        }

        private void CreateCookieForTokens(string access, string refresh, string deviceId)
        {
            bool isDevelopment = _env.IsDevelopment();
            Response.Cookies.Append("access", access, new CookieOptions
            {
                HttpOnly = true,
                Secure = !isDevelopment,
                SameSite = isDevelopment ? SameSiteMode.Strict : SameSiteMode.None,
                Expires = DateTime.UtcNow.AddMinutes(Double.Parse(_configuration["Jwt:ExpireMinutesAccess"]))
            });

            Response.Cookies.Append("refresh", refresh, new CookieOptions
            {
                HttpOnly = true,
                Secure = !isDevelopment,
                SameSite = isDevelopment ? SameSiteMode.Strict : SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(Double.Parse(_configuration["Jwt:ExpireDaysRefresh"]))
            });

            Response.Cookies.Append("deviceId", deviceId, new CookieOptions
            {
                HttpOnly = true,
                Secure = !isDevelopment,
                SameSite = isDevelopment ? SameSiteMode.Strict : SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(Double.Parse(_configuration["Jwt:ExpireDaysRefresh"]))
            });
        }

        [HttpGet("cleanup-token")]
        public async Task<IActionResult> CleanupToken()
        {
            var quantity = await _refresh.DeleteExpiredOrRevokedTokensAsync();
            var response = new CleanupResponse
            {
                Quantity = quantity
            };
            return Ok(ControllerResponse<CleanupResponse>.Ok(response, "Cleanup thành công"));
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromHeader(Name = "X-User-Id")] int userId)
        {
            Response.Cookies.Delete("access");

            if (HttpContext.Request.Cookies.TryGetValue("refresh", out var refresh) && HttpContext.Request.Cookies.TryGetValue("deviceId", out var deviceId))
            {
                Response.Cookies.Delete("refresh");
                Response.Cookies.Delete("deviceId");
                var logout = await _auth.Logout(deviceId, refresh);
                if (!logout.IsSuccess) return Ok(ControllerResponse<string>.Ok(null, "Đăng xuất thành công nhưng chưa revoke được token"));
            }

            return Ok(ControllerResponse<string>.Ok(null, "Đăng xuất thành công"));
        }


        [HttpGet("me")]
        public async Task<IActionResult> Me([FromHeader(Name = "X-User-Id")] int userId)
        {
            var findById = await _user.FindByIdAsync(userId);
            if (!findById.IsSuccess) return BadRequest(ControllerResponse<LoginUserDTO>.Fail(findById.Message));
            var user = findById.Data;

            var response = _mapper.Map<LoginUserDTO>(user);
            return Ok(ControllerResponse<LoginUserDTO>.Ok(response, "Tái đăng nhập thành công"));
        }
    }
}