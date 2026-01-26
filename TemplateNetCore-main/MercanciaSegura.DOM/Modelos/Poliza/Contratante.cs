using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MercanciaSegura.DOM.Modelos.Poliza
{
    [Table("Contratante")]
    public class Contratante
    {
        [Key]
        [Column("Contratante_ID")]
        public int ContratanteId { get; set; }

        [Column("Nombre")]
        [MaxLength(100)]
        public string? Nombre { get; set; }
    }
}
