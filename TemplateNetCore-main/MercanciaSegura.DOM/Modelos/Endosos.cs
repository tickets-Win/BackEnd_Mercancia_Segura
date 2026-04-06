using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercanciaSegura.DOM.Modelos
{
    [Table("Endosos")]
    public class Endosos
    {
        [Key]
        [Column("Endoso_ID")]
        public int EndosoId { get; set; }

        [Column("Tipo_Endoso_ID")]
        public int TipoEndosoId { get; set; }

        [ForeignKey(nameof(TipoEndosoId))]
        public TipoEndoso? TipoEndoso { get; set; }

        [Column("Numero_Endoso")]
        [MaxLength(100)]
        public string? NumeroEndoso { get; set; }

        [Column("Certificado_ID")]
        public int CertificadoId { get; set; }

        [ForeignKey(nameof(CertificadoId))]
        public Certificado? Certificado { get; set; }

        [Column("Fecha_Elaboracion")]
        public DateTime FechaElaboracion { get; set; }

        [Column("Agente")]
        [MaxLength(150)]
        public string? Agente { get; set; }

        [Column("RFC")]
        [MaxLength(50)]
        public string? RFC { get; set; }

        [Column("Oficina")]
        [MaxLength(150)]
        public string? Oficina { get; set; }

        [Column("BeneficiarioPreferente")]
        [MaxLength(200)]
        public string? BeneficiarioPreferente { get; set; }

        [Column("Moneda_ID")]
        public int? MonedaId { get; set; }

        [Column("IVA", TypeName = "decimal(18,2)")]
        public decimal? IVA { get; set; }

        [Column("Total_a_pagar", TypeName = "decimal(18,2)")]
        public decimal? TotalAPagar { get; set; }

        [Column("Descripcion")]
        [MaxLength(300)]
        public string? Descripcion { get; set; }
    }
}