using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercanciaSegura.DOM.Modelos.Cliente
{
    [Table("Cuota")]
    public class Cuota
    { 
        [Key]
        [Column("Cuota_ID")]
        public int CuotaId { get; set; }

        [Column("Tipo_Cuota_ID")]
        public int? TipoCuotaId { get; set; }

        [Column("Monto", TypeName = "decimal(10,2)")]
        public decimal? Monto { get; set; }

        [Column("Cliente_ID")]
        public int? ClienteId { get; set; }

        [Column("Tipo_Tarifa_ID")]
        public int? TipoTarifaId { get; set; }

        [ForeignKey(nameof(ClienteId))]
        public Cliente? Cliente { get; set; }

        [ForeignKey(nameof(TipoCuotaId))]
        public TipoCuota? TipoCuota { get; set; }

        [ForeignKey(nameof(TipoTarifaId))]
        public TipoTarifa? TipoTarifa { get; set; }

    }
}
