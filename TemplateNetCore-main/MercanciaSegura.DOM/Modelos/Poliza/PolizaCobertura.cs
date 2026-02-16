using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercanciaSegura.DOM.Modelos.Poliza
{
    [Table("Poliza_Cobertura")]
    public class PolizaCobertura
    {
        [Key]
        [Column("Poliza_Cobertura_ID")]
        public int PolizaCoberturaId { get; set; }

        [Column("Poliza_ID")]
        public int PolizaId { get; set; }

        [Column("Deducible")]
        [MaxLength(350)]
        public string? Deducible { get; set; }

        [Column("Prima", TypeName = "decimal(12,2)")]
        public decimal? Prima { get; set; }

        [Column("Suma_Asegurada", TypeName = "decimal(14,2)")]
        public decimal? SumaAsegurada { get; set; }

        [ForeignKey(nameof(PolizaId))]
        public Poliza? Poliza { get; set; }
    }
}
