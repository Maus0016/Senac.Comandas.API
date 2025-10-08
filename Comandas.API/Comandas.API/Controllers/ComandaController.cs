using Comandas.API.DTOs;
using Comandas.API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandaController : ControllerBase
    {
       
        public List<Comanda> comandas = new List<Comanda>
        {
            new Comanda
            {
                Id = 1,
                NomeCliente = "João Silva",
                NumeroMesa = 5
            },
            new Comanda
            {
                Id = 2,
                NomeCliente = "Maria Oliveira",
                NumeroMesa = 3
            },

        };

        [HttpGet]
        public IResult Get()
        {
            return Results.Ok(comandas);
        }

        // GET api/<ComandaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var comanda = comandas.FirstOrDefault(c => c.Id == id);
       
            if (comanda is null)
            {
                return Results.NotFound("Comanda não encontrada");
            }
            return Results.Ok(comanda);
        }


        // POST api/<ComandaController>
        [HttpPost]
        public IResult Post([FromBody] ComandaCreateRequest comandaCreate)
        {
            if (comandaCreate.NomeCliente.Length < 3) 
                return Results.BadRequest("O nome do cliente deve ter pelo menos 3 caracteres");
            if (comandaCreate.NumeroMesa <= 0)
                return Results.BadRequest("O número da mesa deve ser maior que zero");
            if (comandaCreate.CardapioItemIds.Length == 0)
                return Results.BadRequest("A comanda deve ter pelo menos um item do cardápio");
            var novaComanda = new Comanda
            {
                Id = comandas.Count + 1,
                NomeCliente = comandaCreate.NomeCliente,
                NumeroMesa = comandaCreate.NumeroMesa,
            };
            comandas.Add(novaComanda);
            return Results.Created($"/api/comanda/{novaComanda.Id}", novaComanda);

        }

        // PUT api/<ComandaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ComandaUpdateRequest comandaUpdate)
        {
        }

        // DELETE api/<ComandaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
