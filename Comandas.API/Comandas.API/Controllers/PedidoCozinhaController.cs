using Comandas.API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoCozinhaController : ControllerBase
    {

        public List<PedidoCozinha> pedidoCozinha = new List<PedidoCozinha>
        {
            new PedidoCozinha
            {
                Id = 1,
                ComandaId = 1,
    
            },
            new PedidoCozinha
            {
                Id = 2,
                ComandaId = 2,
    
            },
        };
        // GET: api/<PedidoCozinhaController>
        [HttpGet]
        public IResult GetPedidoCozinha()
        {
            return Results.Ok(pedidoCozinha);
        }
        
        // GET api/<PedidoCozinhaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var pedidocozinha = pedidoCozinha.FirstOrDefault(p => p.Id == id);
            if (pedidocozinha is null)
            {
                return Results.NotFound("Pedido de cozinha não encontrado");
            }
            return Results.Ok(pedidocozinha);
        }

        // POST api/<PedidoCozinhaController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PedidoCozinhaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PedidoCozinhaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
