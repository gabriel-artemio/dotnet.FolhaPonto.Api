using FolhaPonto.Api.BLL;
using FolhaPonto.Api.Models;
using FolhaPonto.Api.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FolhaPonto.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroPontoController : ControllerBase
    {
        private readonly RegistroPontoBLL bll;
        public RegistroPontoController(IConfiguration configuration)
        {
            bll = new RegistroPontoBLL(configuration);
        }

        [HttpGet]
        public List<RegistroPonto>? GetAll()
        {
            return bll.GetAll();
        }

        [HttpGet("{id}")]
        public RegistroPonto? GetById(string id)
        {
            int _id = 0;
            RegistroPonto? registroPonto = null;
            if (int.TryParse(id, out _id))
            {
                registroPonto = bll.GetById(_id);
            }
            return registroPonto;
        }

        [HttpGet("byFuncionario/{id}")]
        public List<RegistroPonto>? GetByFuncionario(string id)
        {
            int _id = 0;
            List<RegistroPonto>? registroPonto = null;
            if (int.TryParse(id, out _id))
            {
                registroPonto = bll.GetByFuncionario(_id);
            }
            return registroPonto;
        }
        [HttpGet("horasByFuncionario/{id}")]
        public HorasCalculadas GetHorasByFuncionario(string id)
        {
            int _id = 0;
            List<RegistroPonto>? registroPonto = null;
            CalculoHorasService calculoHorasService = new CalculoHorasService();
            if (int.TryParse(id, out _id))
            {
                registroPonto = bll.GetByFuncionario(_id);
            }

            var horasCalculadas = calculoHorasService.CalcularHoras(registroPonto);
            var horasAlmocoCalculadas = calculoHorasService.CalcularHorasAlmoco(registroPonto);

            return new HorasCalculadas() { 
                registros = registroPonto, 
                horasCalculadas = horasCalculadas, 
                horasAlmocoCalculadas = horasAlmocoCalculadas 
            };
        }

        [HttpGet("horasExtrasByFuncionario/{id}/{status}")]
        public HorasExtrasCalculadas GetHorasExtras(string id, string status)
        {
            int _status = 0;
            int _id = 0;
            List<RegistroPonto>? registroPonto = null;
            CalculoHorasService calculoHorasService = new CalculoHorasService();
            if (int.TryParse(status, out _status) && int.TryParse(id, out _id))
            {
                registroPonto = bll.GetHorasExtras(_id, _status);
            }

            var horasExtrasCalculadas = calculoHorasService.CalcularHorasExtrasAprovadas(registroPonto);

            return new HorasExtrasCalculadas()
            {
                registros = registroPonto,
                horasExtrasCalculadas = horasExtrasCalculadas
            };
        }
        [HttpPost]
        public dynamic Insert([FromBody] RegistroPonto registroPonto)
        {
            if (registroPonto != null)
            {
                try
                {
                    bll.Insert(registroPonto);
                    return StatusCode(200);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest("Não foram informados dados");
            }
        }
    }
}