using Comandas.API.DTOs;
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
        public IResult Post([FromBody] PedidoCozinhaCreateRequest pedidoCozinhaCreate)
        {
            if (pedidoCozinhaCreate.ComandaId <= 0)
                Results.BadRequest("O id da comanda deve ser maior que zero.");
            var pedidocozinha = new PedidoCozinha
            {
                Id = pedidoCozinha.Count + 1,
                ComandaId = pedidoCozinhaCreate.ComandaId,
            };
            pedidoCozinha.Add(pedidocozinha);
           return Results.Created($"/api/pedidocozinha/{pedidocozinha.Id}", pedidocozinha);
        }

        // PUT api/<PedidoCozinhaController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] PedidoCozinhaUpdateRequest pedidoCozinhaUpdate)
        {
            var pedidocozinha = pedidoCozinha.FirstOrDefault(p => p.Id == id);
            if (pedidocozinha is null)
               return Results.NotFound($"Pedido de cozinha do id {id} não encontrado");
            pedidocozinha.ComandaId = pedidoCozinhaUpdate.ComandaId;
            return Results.Ok(pedidocozinha);
        }

        // DELETE api/<PedidoCozinhaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
