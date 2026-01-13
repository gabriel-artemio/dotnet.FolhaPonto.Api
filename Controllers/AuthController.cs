using estoque_api.BLL;
using estoque_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace estoque_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UsuarioBLL bll;
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            bll = new UsuarioBLL(configuration);
            _configuration = configuration;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] Login login)
        {
            var user = bll.GetUser(login.nm_usuario, login.senha);

            if (user == null)
            {
                return Unauthorized();
            }
            var token = GenerateJwtToken(user);
            return Ok(new { token });
        }
        private string GenerateJwtToken(Usuario user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            string secretKey = _configuration["JwtSettings:Key"];
            var key = Encoding.UTF8.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.nm_usuario),
                    new Claim(ClaimTypes.Role, user.permissao)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = "gocode.com",
                Audience = "gocode.com",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}