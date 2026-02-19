using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercanciaSegura.DOM.Modelos.Poliza
{
    [Table("Bien")]
    public class Bien
    {
        [Key]
        [Column("Bien_ID")]
        public int BienId { get; set; }

        [Column("Poliza_ID")]
        public int? PolizaId { get; set; }

        [Column("Tipo_Bien")]
        public int? TipoBienId { get; set; }

        [Column("Nombre")]
        [MaxLength(250)]
        public string? Nombre { get; set; }

        [ForeignKey(nameof(PolizaId))]
        public Poliza? Poliza { get; set; }

        [ForeignKey(nameof(TipoBienId))]
        public TipoBien? TipoBien { get; set; }
    }
}
