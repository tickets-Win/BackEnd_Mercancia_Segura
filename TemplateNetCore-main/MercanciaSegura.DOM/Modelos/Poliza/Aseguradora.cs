using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercanciaSegura.DOM.Modelos.Poliza
{
    [Table("Aseguradora")]
    public class Aseguradora
    {
        [Key]
        [Column("Aseguradora_ID")]
        public int AseguradoraId { get; set; }

        [Column("Nombre")]
        [MaxLength(100)]
        public string? Nombre { get; set; }
    }
}
