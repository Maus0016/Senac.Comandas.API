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
        //METODO GET que retorna a lista de cardapio

        // GET: api/<CardapioItemController>
        [HttpGet] // ANOTAÇÃO QUE INDICA SE O METODO RESPONDE A REQUISIÇÃO HTTP GET
        public IEnumerable<CardapioItem> Get()
        {
            // CRIA UMA LISTA ESTATICA DE CARDAPIO e TRANSFORMA EM JSON
            return new CardapioItem[]
               { new CardapioItem // CRIA O PRIMEIRO ELEMENTO DA LISTA DE CARDAPIO
{                Id = 1,
                    Titulo = "Coca-Cola Lata",
                    Descricao = "Refrigerante Coca-Cola Lata 350ml",
                    Preco = 5.00M,
                    PossuiPreparo = true
                },
            new CardapioItem
            {
                Id = 2,
                Titulo = "Fanta Lata",
                Descricao = "Refrigerante Fanta Lata 350ml",
                Preco = 5.00M,
                PossuiPreparo = true
            },
        };
               }

        // GET api/<CardapioItemController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CardapioItemController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CardapioItemController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CardapioItemController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
