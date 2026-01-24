using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shelf.Infrastructure.Entity;
using Shelf.Core.Enum;
namespace Shelf.Core.Services;

public interface ITokenService
{
    string GenerateUserToken(User user, IList<string> roles);
    string GenerateShareToken(string shareId);
}

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    private readonly SymmetricSecurityKey _key;

    public TokenService(IConfiguration config)
    {
        _config = config;
        _key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config["JWT:SecretKey"]!));
    }

    public string GenerateUserToken(User user, IList<string> roles)
    {
        List<Claim> claims = new List<Claim>
           {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName!),
                new Claim("access_type", TokenUseType.USERACCESS.ToString()),
           };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        SigningCredentials creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = creds,
            Issuer = _config["JWT:Issuer"],
            Audience = _config["JWT:Audience"],
        };

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public string GenerateShareToken(string shareId)
    {
        List<Claim> claims = new List<Claim>
           {
                new Claim("share_id", shareId),
                new Claim("access_type", TokenUseType.CONTENTACCESS.ToString()),
           };

        SigningCredentials creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = creds,
            Issuer = _config["JWT:Issuer"],
            Audience = _config["JWT:Audience"],
        };

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }


}