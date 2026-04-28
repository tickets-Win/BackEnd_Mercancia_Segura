using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercanciaSegura.DOM.Modelos.Cotizacion
{
    [Table("Cotizacion_Mercancia")]
    public class CotizacionMercancia
    {
        [Key]
        [Column("Cotizacion_Mercancia_ID")]
        public int CotizacionMercanciaId { get; set; }

        [Column("Cotizacion_ID")]
        public int CotizacionId { get; set; }

        [ForeignKey(nameof(CotizacionId))]
        public Cotizacion? Cotizacion { get; set; }

        [Column("Cotizacion_Cliente")]
        [MaxLength(100)]
        public string? CotizacionCliente { get; set; }

        [Column("Transito")]
        [MaxLength(50)]
        public string? Transito { get; set; }

        [Column("Clasificacion_ID")]
        public int? ClasificacionId { get; set; }

        [ForeignKey(nameof(ClasificacionId))]
        public Clasificacion? Clasificacion { get; set; }

        [Column("SubClasificacion")]
        public string? SubClasificacion { get; set; }

        [Column("Descripcion_Mercancia")]
        public string? DescripcionMercancia { get; set; }

        [Column("Tipo_Empaque")]
        public string? TipoEmpaque { get; set; }

        [Column("Origen")]
        public string? Origen { get; set; }

        [Column("Destino")]
        public string? Destino { get; set; }

        [Column("Medio_De_Conduccion")]
        public string? MedioDeConduccion { get; set; }

        [Column("Medio_De_Transporte")]
        public string? MedioDeTransporte { get; set; }

        [Column("Observaciones")]
        public string? Observaciones { get; set; }

        [Column("Medidas_De_Seguridad_Adicionales")]
        public string? MedidasDeSeguridadAdicionales { get; set; }

        [Column("Deducibles")]
        public string? Deducibles { get; set; }

        [Column("Moneda_Cuota_Aplicable_ID")]
        public int? MonedaCuotaAplicableId { get; set; }

        [Column("Cuota_Aplicable", TypeName = "decimal(18,4)")]
        public decimal? CuotaAplicable { get; set; }

        [Column("Moneda_Cuota_Minima_ID")]
        public int? MonedaCuotaMinimaId { get; set; }

        [Column("Cuota_Minima", TypeName = "decimal(18,4)")]
        public decimal? CuotaMinima { get; set; }

        [Column("Tipo_Cambio_Cotizar", TypeName = "decimal(18,4)")]
        public decimal? TipoCambioCotizar { get; set; }
        
        [Column("Moneda_Cotizar_ID")]
        public int? MonedaCotizarId { get; set; }

        [Column("Suma_Asegurada", TypeName = "decimal(18,2)")]
        public decimal? SumaAsegurada { get; set; }
    }
}