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
        //VARIAVEL QUE REPRESENTA O BANCO DE DADOS
        public ComandasDbContext _context { get; set; }

        //Construtor 
        public UsuarioController(ComandasDbContext context)
        {
            _context = context;
        }
    
        [HttpGet]
        public IResult Get()
            //Conectar com o banco
            // executar a consulta SELECT * FROM USUARIOS
        {
            var usuarios = _context.Usuarios.ToList();

            return Results.Ok(usuarios);
        }

        //Iresult que retorna um usuario pelo id
        // GET api/<UsuarioController>/5

        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var usuarios = _context.Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuarios is null)
            {
                return Results.NotFound($"Usuario do id {id} não encontrado");
            }
            return Results.Ok(usuarios);
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
                Nome = usuarioCreate.Nome,
                Email = usuarioCreate.Email,
                Senha = usuarioCreate.Senha
            };

            //ADICIONA O USUARIO NA LISTA
            //Adiciona o usuario no contexto do banco de dados
            _context.Usuarios.Add(usuario);

            //Executa o INSERT INTO Usuarios (Id, Nome, Email, Senha) VALUES (...)
            _context.SaveChanges();
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
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
            //Se não encontrar retorna not found
            if (usuario is null)
            
                return Results.NotFound($"Usuario do id {id} não encontrado.");

            // Atualiza o Usuaruio
                usuario.Nome = usuarioUpdate.Nome;
                usuario.Email = usuarioUpdate.Email;
                usuario.Senha = usuarioUpdate.Senha;

            //Update Usuario set Nome = ..., Email = ..., Senha = ... where Id = ...
            _context.SaveChanges();
            //Retorna no Context
            return Results.NoContent(); 
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            var user = _context.Usuarios.FirstOrDefault(c => c.Id == id);
            if(user is null)
                return Results.NotFound($"Usuario do {id} não encontrado");

           _context.Usuarios.Remove(user);
            var removido = _context.SaveChanges();
            if (removido > 0)
            {
                return Results.NoContent();
            }

            return Results.StatusCode(500);
        }
    }
}
