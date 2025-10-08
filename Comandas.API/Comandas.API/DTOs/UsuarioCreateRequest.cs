namespace Comandas.API.DTOs
{
    public class UsuarioCreateRequest
    {
        public string Nome { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Senha { get; set; } = default!;
    }
}
