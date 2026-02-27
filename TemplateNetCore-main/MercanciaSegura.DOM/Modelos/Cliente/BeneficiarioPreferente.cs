using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercanciaSegura.DOM.Modelos.Cliente
{
    [Table("Beneficiario_Preferente")]
    public class BeneficiarioPreferente
    {
        [Key]
        [Column("Beneficiario_Preferente_ID")]
        public int BeneficiarioPreferenteId { get; set; }

        [Column("Nombre")]
        [MaxLength(100)]
        public string? Nombre { get; set; }

        [Column("Domicilio")]
        [MaxLength(200)]
        public string? Domicilio { get; set; }

        [Column("RFC")]
        [MaxLength(13)]
        public string? RFC { get; set; }

        [Column("Clave")]
        [MaxLength(10)]
        public string? Clave { get; set; }

        [Column("Tipo_Persona_ID")]
        public int? TipoPersonaId { get; set; }

        [Column("Apellido_Paterno")]
        [MaxLength(60)]
        public string? ApellidoPaterno { get; set; }

        [Column("Apellido_Materno")]
        [MaxLength(60)]
        public string? ApellidoMaterno { get; set; }

        [Column("Nombre_Completo")]
        [MaxLength(120)]
        public string? NombreCompleto { get; set; }

        [Column("RfcGenerico_ID")]
        public int? RfcGenericoId { get; set; }

        [Column("Calle")]
        [MaxLength(120)]
        public string? Calle { get; set; }

        [Column("Numero_Int")]
        [MaxLength(10)]
        public string? NumeroInt { get; set; }

        [Column("Numero_Ext")]
        [MaxLength(10)]
        public string? NumeroExt { get; set; }

        [Column("Poblacion")]
        [MaxLength(80)]
        public string? Poblacion { get; set; }

        [Column("Colonia")]
        [MaxLength(60)]
        public string? Colonia { get; set; }

        [Column("CP")]
        [StringLength(5)]
        public string? Cp { get; set; }

        [Column("Pais")]
        [MaxLength(50)]
        public string? Pais { get; set; }

        [Column("Nacionalidad")]
        [MaxLength(30)]
        public string? Nacionalidad { get; set; }

        [Column("Fecha_Actualizacion")]
        public DateTime? FechaActualizacion { get; set; }

        [Column("Fecha_Registro")]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [ForeignKey(nameof(TipoPersonaId))]
        public TipoPersona? TipoPersona { get; set; }

        [ForeignKey(nameof(RfcGenericoId))]
        public RfcGenerico? RfcGenerico { get; set; }
    }
}
