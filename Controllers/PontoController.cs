using FolhaPonto.Api.DAL;
using FolhaPonto.Api.Models;
using FolhaPonto.Api.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FolhaPonto.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PontoController : ControllerBase
    {
        private readonly RegistroPontoRepository _repo;
        public PontoController(RegistroPontoRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("registrar")]
        public IActionResult Registrar(RegistroPonto registroPonto)
        {
            var registro = new RegistroPonto
            {
                FuncionarioId = registroPonto.FuncionarioId,
                DataHora = DateTime.UtcNow,
                Tipo = registroPonto.Tipo
            };

            _repo.Inserir(registro);
            return Ok(registro);
        }

        [HttpGet("horas-dia")]
        public IActionResult HorasDia(DateTime data)
        {
            var funcionarioId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var registros = _repo.ObterPorDia(funcionarioId, data);
            var total = new CalculoHorasService().CalcularHoras(registros);

            return Ok(new { horas = total.TotalHours });
        }
    }
}