using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercanciaSegura.DOM.Modelos
{
    /// <summary>
    /// Certificado que se genera al confirmar una cotización.
    ///
    /// OJO: esta clase se reconstruyó a partir del esquema real de la tabla en la
    /// base, porque las migraciones que la reformaron (CreatreTablesUltimatesV1,
    /// UpdateTableCDyC y QuitarClienteIdCertificado) están aplicadas en el
    /// servidor pero no existen en este repositorio. Cuando esas migraciones se
    /// integren, este archivo va a chocar en el merge: hay que quedarse con la
    /// versión que venga con ellas.
    /// </summary>
    [Table("Certificado")]
    public class Certificado
    {
        [Key]
        [Column("Certificado_ID")]
        public int CertificadoId { get; set; }

        [Column("Cotizacion_ID")]
        public int CotizacionId { get; set; }

        [ForeignKey(nameof(CotizacionId))]
        public Cotizacion.Cotizacion? Cotizacion { get; set; }

        /// <summary>Cuándo se generó el certificado.</summary>
        [Column("Fecha_Registro")]
        public DateTime FechaRegistro { get; set; }

        [Column("Asegurado")]
        [MaxLength(200)]
        public string? Asegurado { get; set; }

        [Column("Clave_Certificado")]
        [MaxLength(50)]
        public string? ClaveCertificado { get; set; }

        /// <summary>Vigencia, copiada de la cotización que le dio origen.</summary>
        [Column("Fecha_Inicio")]
        public DateTime FechaInicio { get; set; }

        [Column("Fecha_Fin")]
        public DateTime FechaFin { get; set; }

        [Column("Suma_Asegurada", TypeName = "decimal(18,2)")]
        public decimal SumaAsegurada { get; set; }

        [Column("Tipo_Estatus_ID")]
        public int? TipoEstatusId { get; set; }

        [ForeignKey(nameof(TipoEstatusId))]
        public Cliente.TipoEstatus? TipoEstatus { get; set; }
    }
}
