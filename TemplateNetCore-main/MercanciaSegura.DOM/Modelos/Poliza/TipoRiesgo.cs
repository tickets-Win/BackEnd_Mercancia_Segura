using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercanciaSegura.DOM.Modelos.Poliza
{
    [Table("Tipo_Riesgo")]
    public class TipoRiesgo
    {
        [Key]
        [Column("Tipo_Riesgo_ID")]
        public int TipoRiesgoId { get; set; }

        [Column("Nombre")]
        [MaxLength(100)]
        public string? Nombre { get; set; }
    }
}
