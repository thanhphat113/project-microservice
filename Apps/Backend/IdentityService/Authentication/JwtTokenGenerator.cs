using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace IdentityService.Authentication;

public class JwtToken
{
    private readonly IConfiguration _configuration;

    public JwtToken(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateJwtToken(int userId, bool isAccess = true)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
        };

        var keyValue = _configuration["Jwt:Key"];

        if (string.IsNullOrEmpty(keyValue))
        {
            Console.WriteLine("Jwt:Key cannot be null or empty");
        }


        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyValue));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: isAccess ? DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireMinutesAccess"]))
                                : DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["Jwt:ExpireMinutesRefresh"])),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
