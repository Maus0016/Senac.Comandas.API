using Comandas.API.DTOs;
using Comandas.API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas.API.Controllers
{
    //CRIA A ROTA DO CONTROLADOR
    [Route("api/[controller]")]
    [ApiController] // DEFINE QUE ESSA CLASSE É UM CONTROLADOR DE API
    public class CardapioItemController : ControllerBase // MEDIA BASE DE ControllerBase para PODER  RESPONDER A REQUESITOS HTTP
    {

       private readonly ComandasDbContext _context;

        public CardapioItemController(ComandasDbContext context)
        {
            _context = context;
        }

        //METODO GET que retorna a lista de cardapio

        // GET: api/<CardapioItemController>
        [HttpGet] // ANOTAÇÃO QUE INDICA SE O METODO RESPONDE A REQUISIÇÃO HTTP GET
        public IResult GetCardapios()
        {
            // CRIA UMA LISTA ESTATICA DE CARDAPIO e TRANSFORMA EM JSON
            var cardapios = _context.CardapioItems.ToList();

            return Results.Ok(cardapios);
        }


        // GET api/<CardapioItemController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            //BUSCAR NA LISTA DE CARDAPIO DE ACORDO COM id DO PARAMETRO
            // JOGA O VALOR PARA A VARIAVEL O PRIMERIRO ELEMENTO DE ACORDO COM O 
            var cardapio = _context.CardapioItems.FirstOrDefault(c => c.Id == id);
            //RETORNA O VALOR PARA O ENDPOINT DA API

            if (cardapio is null)
            {
                return Results.NotFound("Cardapio não encontrado");
            }

            //RETORNA O VALOR PARA O ENDPOINT DA API
            return Results.Ok(cardapio);
        }

        // POST api/<CardapioItemController>
        [HttpPost]
        public IResult Post([FromBody] CardapioItemCreateRequest cardapio)
        {
            if (cardapio.Titulo.Length < 3)
                Results.BadRequest("O titulo deve ter no minimo 3 caracteres.");
            if (cardapio.Descricao.Length < 5)
                Results.BadRequest("A descricao do cardápio deve ter no minimo 5 caracteres.");
            if (cardapio.Preco <= 0)
                Results.BadRequest("O preço deve ser maior que zero.");
            var cardapioItem = new CardapioItem
            {
                Titulo = cardapio.Titulo,
                Descricao = cardapio.Descricao,
                Preco = cardapio.Preco,
                PossuiPreparo = cardapio.PossuiPreparo
            };
           _context.CardapioItems.Add(cardapioItem);
            _context.SaveChanges();
            return Results.Created($"/api/cardapio/{cardapioItem.Id}", cardapioItem);
        }

        // PUT api/<CardapioItemController>/5
        /// <summary>
        /// Atualiza um cardapio
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cardapio"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] CardapioItemUpdateRequest cardapio)
        {
            var cardapioItem = _context.CardapioItems.FirstOrDefault(c => c.Id == id);

            if (cardapioItem is null)
                 return Results.NotFound($"Cardapio do id {id} não encontrado");
                 cardapioItem.Titulo = cardapio.Titulo;
                 cardapioItem.Descricao = cardapio.Descricao;
                 cardapioItem.Preco = cardapio.Preco;
                 cardapioItem.PossuiPreparo = cardapio.PossuiPreparo;
            _context.SaveChanges();
            return Results.NoContent();

        }

        // DELETE api/<CardapioItemController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            //BUSCAR O CARDAPIO NA LISTA
            var cardapioItem = _context.CardapioItems.FirstOrDefault(c => c.Id == id);
            //SE ESTIVER NULO RETORNA 404
            if (cardapioItem is null)
                return Results.NotFound($"Cardapio do id {id} não encontrado");
                _context.CardapioItems.Remove(cardapioItem);
            var removido = _context.SaveChanges();
            // RETORNA 204 NO CONTENT
            if (removido > 0)
            {
                return Results.NoContent();

            }
            return Results.StatusCode(500);

        }
    }

}
