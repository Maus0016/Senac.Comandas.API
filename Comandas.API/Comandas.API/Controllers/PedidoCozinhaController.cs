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
      public ComandasDbContext _context { get; set; }

        public PedidoCozinhaController(ComandasDbContext context)
        {
            _context = context;
        }
        // GET: api/<PedidoCozinhaController>
        [HttpGet]
        public IResult GetPedidoCozinha()
        {
            var pedidoCozinha = _context.PedidoCozinhas.ToList();
            return Results.Ok(pedidoCozinha);
        }
        
        // GET api/<PedidoCozinhaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var pedidocozinha = _context.PedidoCozinhas.FirstOrDefault(p => p.Id == id);
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
                ComandaId = pedidoCozinhaCreate.ComandaId,
            };
            _context.PedidoCozinhas.Add(pedidocozinha);
            _context.SaveChanges();
            return Results.Created($"/api/pedidocozinha/{pedidocozinha.Id}", pedidocozinha);
        }

        // PUT api/<PedidoCozinhaController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] PedidoCozinhaUpdateRequest pedidoCozinhaUpdate)
        {
            var pedidocozinha = _context.PedidoCozinhas.FirstOrDefault(p => p.Id == id);
            if (pedidocozinha is null)
               return Results.NotFound($"Pedido de cozinha do id {id} não encontrado");
            pedidocozinha.ComandaId = pedidoCozinhaUpdate.ComandaId;

            _context.SaveChanges();

            return Results.NoContent();
        }

        // DELETE api/<PedidoCozinhaController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            var PedidoCozinha = _context.PedidoCozinhas.FirstOrDefault(p => p.Id == id);
            if (PedidoCozinha is null)
                return Results.NotFound($"Pedido de cozinha do id {id} não encontrado");
            _context.PedidoCozinhas.Remove(PedidoCozinha);
            var PedidoCozinhaRemovido = _context.SaveChanges();
            if (PedidoCozinhaRemovido > 0)
            {
                return Results.NoContent();
            }
            return Results.StatusCode(500);

        }
    }
}
