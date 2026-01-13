using estoque_api.BLL;
using estoque_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace estoque_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaidaProdutoController : ControllerBase
    {
        private readonly SaidaProdutoBLL bll;
        public SaidaProdutoController(IConfiguration configuration)
        {
            bll = new SaidaProdutoBLL(configuration);
        }

        [HttpGet]
        public List<SaidaProduto>? Get()
        {
            return bll.GetAll();
        }

        [HttpGet("{id}")]
        public SaidaProduto? Get(string id)
        {
            int _id = 0;
            SaidaProduto? saidaProduto = null;
            if (int.TryParse(id, out _id))
            {
                saidaProduto = bll.GetById(_id);
            }
            return saidaProduto;
        }
        [HttpPost]
        public dynamic Insert([FromBody] SaidaProduto saidaProduto)
        {
            if (saidaProduto != null)
            {
                try
                {
                    bll.Insert(saidaProduto);
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
        public dynamic Put(int id, [FromBody] SaidaProduto saidaProduto)
        {
            saidaProduto.id = id;
            if (saidaProduto != null)
            {
                try
                {
                    bll.Update(saidaProduto);
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