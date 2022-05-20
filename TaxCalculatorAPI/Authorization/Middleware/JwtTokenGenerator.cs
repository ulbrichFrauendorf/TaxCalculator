namespace TaxCalculatorAPI.Authorization.Middleware;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaxCalculatorAPI.Data.Entities;

public interface IJwtTokenGenerator
{
    public string Generate(ApiUser user);
}

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly AppSettings _appSettings;

    public JwtTokenGenerator(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    public string Generate(ApiUser user)
    {
        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.ApiUserId.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}