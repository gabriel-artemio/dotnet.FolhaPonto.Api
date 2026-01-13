using estoque_api.BLL;
using estoque_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace estoque_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstoqueController : ControllerBase
    {
        private readonly EstoqueBLL bll;
        public EstoqueController(IConfiguration configuration)
        {
            bll = new EstoqueBLL(configuration);
        }

        [HttpGet]
        public List<Estoque>? Get()
        {
            return bll.GetAll();
        }

        [HttpGet("{id}")]
        public Estoque? Get(string id)
        {
            int _id = 0;
            Estoque? estoque = null;
            if (int.TryParse(id, out _id))
            {
                estoque = bll.GetById(_id);
            }
            return estoque;
        }
        [HttpPost]
        public dynamic Insert([FromBody] Estoque estoque)
        {
            if (estoque != null)
            {
                try
                {
                    bll.Insert(estoque);
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
        [HttpPut("{id}")]
        public dynamic Put(int id, [FromBody] Estoque estoque)
        {
            estoque.id = id;
            if (estoque != null)
            {
                try
                {
                    bll.Update(estoque);
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
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            bll.Delete(id);
        }
    }
}