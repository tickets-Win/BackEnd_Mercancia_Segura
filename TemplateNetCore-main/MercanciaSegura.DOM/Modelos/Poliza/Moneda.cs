using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercanciaSegura.DOM.Modelos.Poliza
{
    [Table("Moneda")]
    public class Moneda
    {
        [Key]
        [Column("Moneda_ID")]
        public int MonedaId { get; set; }

        [Column("Nombre")]
        [MaxLength(100)]
        public string? Nombre { get; set; }

        [Column("Tipo_Cambio", TypeName = "decimal(18,6)")]
        public decimal? TipoCambio { get; set; }

        [Column("Tipo_Cambio_Ventanilla", TypeName = "decimal(18,6)")]
        public decimal? TipoCambioVentanilla { get; set; }
    }
}
