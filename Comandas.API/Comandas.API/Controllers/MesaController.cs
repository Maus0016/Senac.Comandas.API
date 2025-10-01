using Comandas.API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesaController : ControllerBase
    {
         List<Mesa> mesas = new List<Mesa>()
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
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MesaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MesaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
