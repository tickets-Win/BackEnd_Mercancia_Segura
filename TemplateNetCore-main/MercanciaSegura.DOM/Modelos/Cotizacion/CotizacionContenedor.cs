using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercanciaSegura.DOM.Modelos.Cotizacion
{
    [Table("Cotizacion_Contenedor")]
    public class CotizacionContenedor
    {
        [Key]
        [Column("Cotizacion_Contenedor_ID")]
        public int CotizacionContenedorId { get; set; }

        [Column("Cotizacion_ID")]
        public int CotizacionId { get; set; }

        [ForeignKey(nameof(CotizacionId))]
        public Cotizacion? Cotizacion { get; set; }

        [Column("Tamanio_Contendor_ID")]
        public int? TamanioContendorId { get; set; }

        [ForeignKey(nameof(TamanioContendorId))]
        public TamanioContenedor? TamanioContenedor { get; set; }

        [Column("Numero_contenedor")]
        public int? NumeroContenedor { get; set; }

        [Column("Cuota", TypeName = "decimal(18,2)")]
        public decimal? Cuota { get; set; }

        [Column("Tipo_Contenedor_ID")]
        public int? TipoContenedorId { get; set; }

        [ForeignKey(nameof(TipoContenedorId))]
        public TipoContenedor? TipoContenedor { get; set; }

        [Column("LR", TypeName = "decimal(18,2)")]
        public decimal? LR { get; set; }

        [Column("Referencia")]
        [MaxLength(100)]
        public string? Referencia { get; set; }

        [Column("TC", TypeName = "decimal(18,2)")]
        public decimal? TC { get; set; }

        [Column("Prima_Unitaria_USD", TypeName = "decimal(18,2)")]
        public decimal? PrimaUnitariaUSD { get; set; }  

        [Column("Prima_Unitaria_MXN", TypeName = "decimal(18,2)")]
        public decimal? PrimaUnitariaMXN { get; set; }  

        [Column("Total", TypeName = "decimal(18,2)")]
        public decimal? Total { get; set; }
    }
}