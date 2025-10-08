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

        public List<CardapioItem> cardapios = new List<CardapioItem>()
        {
            new CardapioItem
                {
                Id = 1,
                Titulo = "Xis Salada",
                Descricao = "Pão, salada, ovo, maionese",
                Preco = 27.00M,
                PossuiPreparo = true
            },
            new CardapioItem
            {
                Id = 2,
                Titulo = "Xis Bacon",
                Descricao = "Pão, salada, bacon, ovo e maionese ",
                Preco = 22.00M,
                PossuiPreparo = true
            }
        };

        //METODO GET que retorna a lista de cardapio

        // GET: api/<CardapioItemController>
        [HttpGet] // ANOTAÇÃO QUE INDICA SE O METODO RESPONDE A REQUISIÇÃO HTTP GET
        public IResult GetCardapios()
        {
            // CRIA UMA LISTA ESTATICA DE CARDAPIO e TRANSFORMA EM JSON
            return Results.Ok(cardapios);
        }


        // GET api/<CardapioItemController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            //BUSCAR NA LISTA DE CARDAPIO DE ACORDO COM id DO PARAMETRO
            // JOGA O VALOR PARA A VARIAVEL O PRIMERIRO ELEMENTO DE ACORDO COM O 
            var cardapio = cardapios.FirstOrDefault(c => c.Id == id);
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
        public void Post([FromBody] CardapioItemCreateRequest cardapio)
        {

        }

        // PUT api/<CardapioItemController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] CardapioItemUpdateRequest cardapio)
        {
        }

        // DELETE api/<CardapioItemController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

}
