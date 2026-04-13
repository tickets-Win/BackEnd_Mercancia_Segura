using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercanciaSegura.DOM.Modelos
{
    [Table("Siniestros")]
    public class Siniestros
    {
        [Key]
        [Column("Siniestro_ID")]
        public int SiniestroId { get; set; }

        [Column("Certificado_ID")]
        public int CertificadoId { get; set; }

        [ForeignKey(nameof(CertificadoId))]
        public Certificado? Certificado { get; set; }

        [Column("N_Reporte")]
        [MaxLength(100)]
        public string? NReporte { get; set; }

        [Column("Fecha_Apertura")]
        public DateTime FechaApertura { get; set; }

        [Column("Fecha_Cierre")]
        public DateTime? FechaCierre { get; set; }

        [Column("Tipo_Siniestro_ID")]
        public int? TipoSiniestroId { get; set; }

        [ForeignKey(nameof(TipoSiniestroId))]
        public TipoSiniestro? TipoSiniestro { get; set; }

        [Column("Mercancia")]
        [MaxLength(200)]
        public string? Mercancia { get; set; }

        [Column("Lugar_De_Siniestro")]
        [MaxLength(200)]
        public string? LugarDeSiniestro { get; set; }

        [Column("Suma_Asegurada", TypeName = "decimal(18,2)")]
        public decimal? SumaAsegurada { get; set; }

        [Column("Monto_De_Reclamo", TypeName = "decimal(18,2)")]
        public decimal? MontoDeReclamo { get; set; }

        [Column("Monto_De_Indemnizacion", TypeName = "decimal(18,2)")]
        public decimal? MontoDeIndemnizacion { get; set; }

        [Column("Tipo_de_evento_ID")]
        [MaxLength(50)]
        public string? TipoDeEventoId { get; set; }
    }
}