using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VokzFeedback.Data;
using VokzFeedback.Models;

namespace VokzFeedback.Services
{
    public class TokenService
    {

        private readonly BancoContext _context;
        private readonly IConfiguration _configuration;
        public TokenService(BancoContext context, IConfiguration configuration) {
            _context = context;
            _configuration = configuration;
        }

        public string GenerateToken(Usuario usuario)
        {

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? string.Empty));
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var expiresIn = DateTime.Now.AddHours(2);
                
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);    

            var tokenOptions = new JwtSecurityToken(issuer, audience, claims: new[]
            {
                new Claim(type: "Id", usuario.Id.ToString()),
                new Claim(type: ClaimTypes.Name, usuario.Name),
                new Claim(type: ClaimTypes.Email, usuario.Email),
            }, expires: expiresIn, signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return token;

        }

    }
}
