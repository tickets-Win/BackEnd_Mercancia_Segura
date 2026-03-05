using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MercanciaSegura.DOM.Modelos.Cliente;

namespace MercanciaSegura.DOM.Modelos
{
    [Table("Cotizacion")]
    public class Cotizacion
    {
        [Key]
        [Column("Cotizacion_ID")]
        public int CotizacionId { get; set; }

        [Column("Poliza_ID")]
        public int? PolizaId { get; set; }

        [ForeignKey(nameof(PolizaId))]
        public MercanciaSegura.DOM.Modelos.Poliza.Poliza? Poliza { get; set; }

        [Column("Fecha_Cotizacion")]
        public DateTime FechaCotizacion { get; set; }

        [Column("Cliente_ID")]
        public int? ClienteId { get; set; }

        [ForeignKey(nameof(ClienteId))]
        public MercanciaSegura.DOM.Modelos.Cliente.Cliente? Cliente { get; set; }

        [Column("Vigencia_Del")]
        public DateTime? VigenciaDel { get; set; }

        [Column("Vigencia_Hasta")]
        public DateTime? VigenciaHasta { get; set; }

        [Column("Suma_Asegurada", TypeName = "decimal(18,2)")]
        public decimal? SumaAsegurada { get; set; }

        [Column("Gastos_Expedicion", TypeName = "decimal(18,2)")]
        public decimal? GastosExpedicion { get; set; }

        [Column("Motivo_Cancelacion")]
        [MaxLength(200)]
        public string? MotivoCancelacion { get; set; }

        [Column("Fecha_Cancelacion")]
        public DateTime? FechaCancelacion { get; set; }
        public CotizacionMercancia? CotizacionMercancia { get; set; }
        public CotizacionContenedor? CotizacionContenedor { get; set; }
    }
}