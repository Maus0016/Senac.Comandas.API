using Comandas.API.Models;

namespace Comandas.API.DTOs
{
    public class ComandaUpdateRequest
    {
        public int NumeroMesa { get; set; }
        public string NomeCliente { get; set; } = default!;
        public ComandaItemUpdateRequest[] Itens { get; set; } = []; //LISTA

    }
    public class ComandaItemUpdateRequest
    {
        public int Id { get; set; } // ID DA COMANDA ITEM
        public bool Remove { get; set; } // INDICAR SE ESTÁ REMOVENDO
        public int CardapioItemId { get; set; } // INDICAR SE ESTÁ INSERIDO
    }
}
