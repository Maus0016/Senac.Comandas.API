using Comandas.API.Models;

namespace Comandas.API.DTOs
{
    public class ComandaUpdateRequest
    {
        public int NumeroMesa { get; set; }
        public string NomeCliente { get; set; } = default!;
    }
}
