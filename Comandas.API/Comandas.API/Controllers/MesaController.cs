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
         static List<Mesa> mesas = new List<Mesa>()
        {
            new Mesa
                {
                Id = 1,
                NumeroMesa = 1,
                SituacaoMesa = (int)SituacaoMesa.Livre
            },
            new Mesa
            {
                Id = 2,
                NumeroMesa = 2,
                SituacaoMesa = (int)SituacaoMesa.Ocupada
            },
            new Mesa
            {
                Id = 3,
                NumeroMesa = 3,
                SituacaoMesa = (int)SituacaoMesa.Reservada
            }
        };
        // GET: api/<MesaController>
        [HttpGet]
        public IResult Get()
        {
            return Results.Ok(mesas);
        }

        // GET api/<MesaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var mesa = mesas.FirstOrDefault(m => m.Id == id);
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
                Id = mesas.Count + 1,
                NumeroMesa = mesacreate.NumeroMesa,
                SituacaoMesa = mesacreate.SituacaoMesa
            };
            mesas.Add(NovaMesa);
            return Results.Created($"/api/mesa/{NovaMesa.Id}", NovaMesa);
        }

        // PUT api/<MesaController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] MesaUpdateRequest mesaUpdate)
        {
            var mesa = mesas.FirstOrDefault(m => m.Id == id);
            if (mesa is null)
            return Results.NotFound($"Mesa do id {id} não encontrada.");
            mesa.NumeroMesa = mesaUpdate.NumeroMesa;
            mesa.SituacaoMesa = mesaUpdate.SituacaoMesa;
            return Results.NoContent();



        }

        // DELETE api/<MesaController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            var mesa = mesas.FirstOrDefault(m => m.Id == id);
            if (mesa is null)
                return Results.NotFound("Mesa não encontrada.");
            var mesaremovida = mesas.Remove(mesa);
            if(mesaremovida)
                return Results.NoContent();
            return Results.StatusCode(500);

        }
    }
}
