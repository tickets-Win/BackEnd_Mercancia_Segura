using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercanciaSegura.DOM.Modelos.Cotizacion
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
        public Poliza.Poliza? Poliza { get; set; }

        [Column("Fecha_Cotizacion")]
        public DateTime FechaCotizacion { get; set; }

        [Column("Cliente_ID")]
        public int? ClienteId { get; set; }

        [ForeignKey(nameof(ClienteId))]
        public Cliente.Cliente? Cliente { get; set; }

        [Column("Beneficiario_Preferente_ID")]
        public int? BeneficiarioPreferenteId { get; set; }

        [ForeignKey(nameof(BeneficiarioPreferenteId))]
        public Cliente.BeneficiarioPreferente? BeneficiarioPreferente { get; set; }

        [Column("Moneda_ID")]
        public int? MonedaId { get; set; }

        [ForeignKey(nameof(MonedaId))]
        public Poliza.Moneda? Moneda { get; set; }

        [Column("Vigencia_Del")]
        public DateTime? VigenciaDel { get; set; }

        [Column("Vigencia_Hasta")]
        public DateTime? VigenciaHasta { get; set; }

        [Column("Fecha_Cancelacion")]
        public DateTime? FechaCancelacion { get; set; }

        [Column("Prima_Servicio_De_Aseguramiento", TypeName = "decimal(18,4)")]
        public decimal? PrimaServicioDeAseguramiento { get; set; }

        [Column("Subtotal", TypeName = "decimal(18,4)")]
        public decimal? Subtotal { get; set; }

        [Column("IVA", TypeName = "decimal(18,4)")]
        public decimal? IVA { get; set; }

        [Column("Total", TypeName = "decimal(18,4)")]
        public decimal? Total { get; set; }

        [Column("Gastos_Expedicion", TypeName = "decimal(18,2)")]
        public decimal? GastosExpedicion { get; set; }
        public CotizacionMercancia? CotizacionMercancia { get; set; }
        public List<CotizacionContenedor>? CotizacionContenedor { get; set; }
    }
}