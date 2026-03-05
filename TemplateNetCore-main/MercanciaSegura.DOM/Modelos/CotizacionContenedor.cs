using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercanciaSegura.DOM.Modelos
{
    [Table("Cotizacion_Contenedor")]
    public class CotizacionContenedor
    {
        [Key]
        [Column("Cotizacion_Contenedor_ID")]
        public int CotizacionContenedorId { get; set; }

        [Column("Cotizacion_ID")]
        public int CotizacionId { get; set; }

        [Column("Tamaño_Tipo_Contendor")]
        [MaxLength(100)]
        public string? TamanoTipoContendor { get; set; }

        [Column("Numero_contenedor")]
        [MaxLength(100)]
        public string? NumeroContenedor { get; set; }

        [Column("Opcion_Cuota")]
        public decimal? OpcionCuota { get; set; }

        [Column("Tarifa")]
        public decimal? Tarifa { get; set; }

        [Column("Total_Tarifa")]
        public decimal? TotalTarifa { get; set; }

        [Column("IVA")]
        public decimal? IVA { get; set; }

        [Column("Total")]
        public decimal? Total { get; set; }

        // 🔗 Relación con Cotizacion
        [ForeignKey(nameof(CotizacionId))]
        public Cotizacion? Cotizacion { get; set; }
    }
}