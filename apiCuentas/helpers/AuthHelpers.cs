using Entidades;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace apiCuentas.helpers
{
    public class AuthHelpers
    {
        public string GenerateJWTToken(Persona persona)
        {
            var claims = new List<Claim> {
            new Claim(ClaimTypes.Name, persona.Nombre),
            new Claim("id",persona.Id.ToString())
             };
            var jwtToken = new JwtSecurityToken(    
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes("this is my custom Secret key for authentication")
                        ),
                    SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
