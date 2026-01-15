using FolhaPonto.Api.DAL;
using FolhaPonto.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FolhaPonto.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly FuncionarioDAL _repo;
        private readonly IConfiguration _config;

        public AuthController(FuncionarioDAL repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login(string email, string senha)
        {
            var funcionario = _repo.ObterPorEmail(email);
            if (funcionario == null || funcionario.senha != senha)
            {
                return Unauthorized();
            }
                
            var token = GerarToken(funcionario);
            return Ok(new { token });
        }

        [HttpPost("cadastrar")]
        public IActionResult Cadastrar(Funcionario funcionario)
        {
            if (funcionario == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "O funcionário não pode ser nulo" });
            }

            _repo.Inserir(funcionario);
            return Ok(funcionario);
        }

        private string GerarToken(Funcionario f)
        {
            var jwt = _config.GetSection("Jwt");

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, f.id.ToString()),
            new Claim(ClaimTypes.Email, f.email)
        };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwt["Key"]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                jwt["Issuer"],
                jwt["Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(jwt["ExpireMinutes"]!)),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
