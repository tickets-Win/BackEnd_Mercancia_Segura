using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercanciaSegura.DOM.Modelos.Poliza
{
    [Table("Cobertura")]
    public class Cobertura
    {
        [Key]
        [Column("Cobertura_ID")]
        public int CoberturaId { get; set; }

        [Column("Riesgo_ID")]
        public int RiesgoId { get; set; }

        [Column("Poliza_Cobertura_ID")]
        public int PolizaCoberturaId { get; set; }

        [Column("Nombre")]
        [MaxLength(200)]
        public string? Nombre { get; set; }

        [ForeignKey(nameof(RiesgoId))]
        public Riesgo? Riesgo { get; set; }

        [ForeignKey(nameof(PolizaCoberturaId))]
        public PolizaCobertura? PolizaCobertura { get; set; }
    }
}
