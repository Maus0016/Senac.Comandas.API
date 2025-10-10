using Comandas.API.DTOs;
using Comandas.API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        // GET: api/<UsuarioController>
        static List<Usuario> usuarios = new List<Usuario>()
        {
           new Usuario
           {
                Id =   1,
                Nome = "Administador",
                Email = "admin@admin.com",
                Senha = "admin"
           },
           new Usuario
           {
                    Id = 2,
                    Nome = "User",
                    Email = "Daniel99@gmail.com",
                    Senha = "123456"
           }
        };
        [HttpGet]
        public IResult Get()
        {
            return Results.Ok(usuarios);
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
          var usuario = usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario is null)
            {
                return Results.NotFound("Usuario não encontrado");
            }
            return Results.Ok(usuario);
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public IResult Post([FromBody] UsuarioCreateRequest usuarioCreate)
        {
            if(usuarioCreate.Senha.Length < 6) 
               return Results.BadRequest("A senha deve ter no minimo 6 caracteres.");
            if (usuarioCreate.Nome.Length < 3)
                return Results.BadRequest("O nome deve ter no minimo 3 caracteres.");
            if (usuarioCreate.Email.Length < 5 || !usuarioCreate.Email.Contains("@"))
                return Results.BadRequest("O email deve ser válido.");

            var usuario = new Usuario
            {
                Id = usuarios.Count + 1,
                Nome = usuarioCreate.Nome,
                Email = usuarioCreate.Email,
                Senha = usuarioCreate.Senha
            };

            //ADICIONA O USUARIO NA LISTA   
               usuarios.Add(usuario);
             return Results.Created($"/api/usuario/{usuario.Id}", usuario);
        }
        //PUT api/<UsuarioController>/5
        /// <summary>
        /// Atualiza um usuario
        /// </summary>
        /// <param name="id">Id do Usuario</param>
        /// <param name="usuarioUpdate">Dados do Usuario</param>
        /// <returns></returns>

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] UsuarioUpdateRequest usuarioUpdate)
        {
            var usuario = usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario is null)
            
                return Results.NotFound($"Usuario do id {id} não encontrado.");
                usuario.Nome = usuarioUpdate.Nome;
                usuario.Email = usuarioUpdate.Email;
                usuario.Senha = usuarioUpdate.Senha;
            return Results.NoContent();
            
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
