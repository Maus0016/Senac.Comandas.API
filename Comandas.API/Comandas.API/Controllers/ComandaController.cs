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

        static List<Comanda> comandas = new List<Comanda>
        {
            new Comanda
            {
                Id = 1,
                NomeCliente = "João Silva",
                NumeroMesa = 1,
                Items = new List<ComandaItem>
                {
                    new ComandaItem
                    {
                        Id = 1,
                        CardapioItemId = 1,
                        ComandaId = 1,
                    }
                }
            },
            new Comanda
            {
                Id = 2,
                NomeCliente = "Maria Oliveira",
                NumeroMesa = 3,
                Items = new List<ComandaItem>
                {
                    new ComandaItem
                    {   Id = 2,
                        CardapioItemId = 2,
                        ComandaId = 2,
                    }
                }
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
            //CRIA UM VARIAVEL DO TIPO LISTA DE ITENS
            var comandaItens = new List<ComandaItem>();
            //PERCORRE OS IDS DOS ITENS DO CARDAPIO
            foreach (int cardapioItemId in comandaCreate.CardapioItemIds)
            {
                //CRIA UM NOVO ITEM DE COMANDA
                var comandaItem = new ComandaItem
                {
                    Id = comandaItens.Count + 1,
                    CardapioItemId = cardapioItemId,
                    ComandaId = novaComanda.Id,
                };
                //ADICIONA O ITEM NA LISTA DE ITENS
                comandaItens.Add(comandaItem);
            }
            //ATRIBUI A LISTA DE ITENS NA COMANDA
            novaComanda.Items = comandaItens;
            //ADICIONA A COMANDA NA LISTA DE COMANDAS
            comandas.Add(novaComanda);
            return Results.Created($"/api/comanda/{novaComanda.Id}", novaComanda);

        }

        // PUT api/<ComandaController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] ComandaUpdateRequest comandaUpdate)
        {
            var comanda = comandas.FirstOrDefault(c => c.Id == id);
            if (comanda is null)
                //SE NÃO ENCONTROU A COMANDA PESQUISADA

                //RETORNA UM COD 404 NÃO ENCOTRADO
                return Results.NotFound("Comanda não encontrada");

            // VALIDAR O NOME DO CLIENTE
            if (comandaUpdate.NomeCliente.Length < 3)
                return Results.BadRequest("O nome do cliente deve ter pelo menos 3 caracteres");
            // VALIDAR O NÚMERO DA MESA
            if (comandaUpdate.NumeroMesa <= 0)
                return Results.BadRequest("O número da mesa deve ser maior que zero");
            comanda.NumeroMesa = comandaUpdate.NumeroMesa;
            comanda.NomeCliente = comandaUpdate.NomeCliente;
            return Results.NoContent();
        }

        // DELETE api/<ComandaController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            var comanda = comandas.FirstOrDefault(c => c.Id == id);
            if (comanda is null)
                return Results.NotFound("Comanda não encontrada");
            var comandaremovida = comandas.Remove(comanda);
            if (comandaremovida)
                return Results.NoContent();
            return Results.StatusCode(500);
        }
    }
}