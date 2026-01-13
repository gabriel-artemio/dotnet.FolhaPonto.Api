using estoque_api.BLL;
using estoque_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace estoque_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoBLL bll;
        public ProdutoController(IConfiguration configuration)
        {
            bll = new ProdutoBLL(configuration);
        }

        [HttpGet]
        public List<Produto>? Get()
        {
            return bll.GetAll();
        }

        [HttpGet("{id}")]
        public Produto? Get(string id)
        {
            int _id = 0;
            Produto? produto = null;
            if (int.TryParse(id, out _id))
            {
                produto = bll.GetById(_id);
            }
            return produto;
        }
        [HttpPost]
        public dynamic Insert([FromBody] Produto produto)
        {
            if (produto != null)
            {
                try
                {
                    bll.Insert(produto);
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
        public dynamic Put(int id, [FromBody] Produto produto)
        {
            produto.id = id;
            if (produto != null)
            {
                try
                {
                    bll.Update(produto);
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