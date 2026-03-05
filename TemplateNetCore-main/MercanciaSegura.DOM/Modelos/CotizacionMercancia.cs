using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercanciaSegura.DOM.Modelos
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

        [Column("Clasificacion")]
        public string? Clasificacion { get; set; }

        [Column("Sub_Clasificacion")]
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

        [Column("Cuota_Aplicable", TypeName = "decimal(18,4)")]
        public decimal? CuotaAplicable { get; set; }

        [Column("Cuota_Minima", TypeName = "decimal(18,4)")]
        public decimal? CuotaMinima { get; set; }

        [Column("Tipo_Cambio_Cotizar", TypeName = "decimal(18,4)")]
        public decimal? TipoCambioCotizar { get; set; }

        [Column("Prima_Servicio_De_Aseguramiento", TypeName = "decimal(18,4)")]
        public decimal? PrimaServicioDeAseguramiento { get; set; }

        [Column("Subtotal", TypeName = "decimal(18,4)")]
        public decimal? Subtotal { get; set; }

        [Column("IVA", TypeName = "decimal(18,4)")]
        public decimal? IVA { get; set; }

        [Column("Total_Seguro_Mercancia", TypeName = "decimal(18,4)")]
        public decimal? TotalSeguroMercancia { get; set; }

        [Column("Total_Seguro_Contenedor", TypeName = "decimal(18,4)")]
        public decimal? TotalSeguroContenedor { get; set; }

        [Column("Total", TypeName = "decimal(18,4)")]
        public decimal? Total { get; set; }
    }
}