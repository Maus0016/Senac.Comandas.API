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

        public ComandasDbContext _context { get; set; }

        public ComandaController(ComandasDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IResult Get()
        {
            var comandas = _context.Comandas.ToList();
            return Results.Ok(comandas);
        }

        // GET api/<ComandaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var comanda = _context.Comandas.FirstOrDefault(c => c.Id == id);

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
                NomeCliente = comandaCreate.NomeCliente,
                NumeroMesa = comandaCreate.NumeroMesa,
            };

            _context.Comandas.Add(novaComanda);
            _context.SaveChanges();
            return Results.Created($"/api/comanda/{novaComanda.Id}", novaComanda);
            //CRIA UM VARIAVEL DO TIPO LISTA DE ITENS
            
            var comandaItens = new List<ComandaItem>();

            //PERCORRE OS IDS DOS ITENS DO CARDAPIO

            foreach (int cardapioItemId in comandaCreate.CardapioItemIds)
            {
                //CRIA UM NOVO ITEM DE COMANDA
                var comandaItem = new ComandaItem
                {
                    CardapioItemId = cardapioItemId,
                    Comanda = novaComanda,
                };
                //ADICIONA O ITEM NA LISTA DE ITENS
                comandaItens.Add(comandaItem);

                //CRIAR O PEDIDO DE COZINHA E O ITEM DE ACORDO COM O CADASTRO DO CARDAPIO possui preparo

                var cardapioItem = _context.CardapioItems.FirstOrDefault(c => c.Id == cardapioItemId);
            }
            //ATRIBUI A LISTA DE ITENS NA COMANDA
            novaComanda.Items = comandaItens;
            //ADICIONA A COMANDA NA LISTA DE COMANDAS
            _context.Comandas.Add(novaComanda);
            _context.SaveChanges();
            return Results.Created($"/api/comanda/{novaComanda.Id}", novaComanda);

        }

        // PUT api/<ComandaController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] ComandaUpdateRequest comandaUpdate)
        {
            var comanda = _context.Comandas.FirstOrDefault(c => c.Id == id);
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
            var comanda = _context.Comandas.FirstOrDefault(c => c.Id == id);
            if (comanda is null)
                return Results.NotFound("Comanda não encontrada");
            _context.Comandas.Remove(comanda);
            var comandaremovida = _context.SaveChanges();
            if (comandaremovida > 0)
            {
                return Results.NoContent();
            }
            return Results.StatusCode(500);
        }
    }
}