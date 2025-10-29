using System.ComponentModel.DataAnnotations;

namespace Comandas.API.Models
{
    public class CardapioItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public string Titulo { get; set; } = default!;

        public string Descricao { get; set; } = default!;
        public decimal Preco { get; set; }

        public bool PossuiPreparo { get; set; }


    }
}
