using Comandas.API.DTOs;
using Comandas.API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesaController : ControllerBase
    {
      public ComandasDbContext _context { get; set; }

        public MesaController(ComandasDbContext context)
        {
            _context = context;
        }
        // GET: api/<MesaController>
        [HttpGet]
        public IResult Get()
        {
            var mesas = _context.Mesas.ToList();
            return Results.Ok(mesas);
        }

        // GET api/<MesaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var mesa = _context.Mesas.FirstOrDefault(m => m.Id == id);
           if(mesa is null)
            {
                return Results.NotFound("Mesa não encontrada");
            }
            return Results.Ok(mesa);
        }

        // POST api/<MesaController>
        [HttpPost]
        public IResult Post([FromBody] MesaCreateRequest mesacreate)
        {
            if (mesacreate.NumeroMesa <= 0)
                return Results.BadRequest("O número da mesa deve ser maior que zero.");
            var NovaMesa = new Mesa
            {
                NumeroMesa = mesacreate.NumeroMesa,
                SituacaoMesa = mesacreate.SituacaoMesa
            };
            _context.Mesas.Add(NovaMesa);
            _context.SaveChanges();
            return Results.Created($"/api/mesa/{NovaMesa.Id}", NovaMesa);
        }

        // PUT api/<MesaController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] MesaUpdateRequest mesaUpdate)
        {
            var mesa = _context.Mesas.FirstOrDefault(m => m.Id == id);
            if (mesa is null)
            return Results.NotFound($"Mesa do id {id} não encontrada.");
            mesa.NumeroMesa = mesaUpdate.NumeroMesa;
            mesa.SituacaoMesa = mesaUpdate.SituacaoMesa;

            _context.SaveChanges();

            return Results.NoContent();

        }

        // DELETE api/<MesaController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            var mesa = _context.Mesas.FirstOrDefault(m => m.Id == id);
            if (mesa is null)
                return Results.NotFound("Mesa não encontrada.");
            _context.Mesas.Remove(mesa);
            var mesaremovida = _context.SaveChanges();
            if (mesaremovida > 0)
            {
                return Results.NoContent();
            }
            return Results.StatusCode(500);
        }
    }
}
