using Comandas.API.Models;

namespace Comandas.API.DTOs
{
    public class ComandaCreateRequest
    {
        public int NumeroMesa { get; set; }
        public string NomeCliente { get; set; } = default!;

        public int[] CardapioItemIds { get; set; } = default;
    }
}
