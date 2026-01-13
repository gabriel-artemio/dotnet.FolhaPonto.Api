using estoque_api.BLL;
using estoque_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace estoque_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntradaProdutoController : ControllerBase
    {
        private readonly EntradaProdutoBLL entradaProdutobll;
        public EntradaProdutoController(IConfiguration configuration)
        {
            entradaProdutobll = new EntradaProdutoBLL(configuration);
        }

        [HttpGet]
        public List<EntradaProduto>? Get()
        {
            return entradaProdutobll.GetAll();
        }

        [HttpGet("{id}")]
        public EntradaProduto? Get(string id)
        {
            int _id = 0;
            EntradaProduto? entradaProduto = null;
            if (int.TryParse(id, out _id))
            {
                entradaProduto = entradaProdutobll.GetById(_id);
            }
            return entradaProduto;
        }
        [HttpPost]
        public dynamic Insert([FromBody] EntradaProduto entradaProduto)
        {
            if (entradaProduto != null)
            {
                try
                {
                    entradaProdutobll.Insert(entradaProduto);
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
        public dynamic Put(int id, [FromBody] EntradaProduto entradaProduto)
        {
            entradaProduto.id = id;
            if (entradaProduto != null)
            {
                try
                {
                    entradaProdutobll.Update(entradaProduto);
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
            entradaProdutobll.Delete(id);
        }
    }
}