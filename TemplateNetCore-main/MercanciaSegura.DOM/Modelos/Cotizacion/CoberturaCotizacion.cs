using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercanciaSegura.DOM.Modelos.Cotizacion
{
    /// <summary>
    /// Cobertura elegida en una cotizacion. Se guarda como copia y no como
    /// referencia, igual que BienCotizacion: el origen puede ser una Cobertura
    /// (polizas de contenedor) o un RiesgoCubierto (polizas de mercancia), asi que
    /// conservar solo el nombre evita depender de dos catalogos distintos.
    /// </summary>
    [Table("Cobertura_Cotizacion")]
    public class CoberturaCotizacion
    {
        [Key]
        [Column("Cobertura_Cotizacion_ID")]
        public int CoberturaCotizacionId { get; set; }

        [Column("Cotizacion_ID")]
        public int? CotizacionId { get; set; }

        [Column("Nombre", TypeName = "nvarchar(max)")]
        public string? Nombre { get; set; }

        [ForeignKey(nameof(CotizacionId))]
        public Cotizacion? Cotizacion { get; set; }
    }
}
