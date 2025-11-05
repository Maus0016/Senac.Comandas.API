using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comandas.API.Models
{
    public class PedidoCozinha
    {
        [Key] //PK
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ComandaId { get; set; } //FK
        public virtual Comanda Comanda{ get; set; } // Navegação
        public List<PedidoCozinhaItem> Itens { get; set; } = [];
    }
}
