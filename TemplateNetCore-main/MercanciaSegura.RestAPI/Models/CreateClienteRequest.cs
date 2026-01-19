using System.ComponentModel.DataAnnotations;

namespace MercanciaSegura.RestAPI.Models
{
    public class CreateClienteRequest
    {
        [Required]
        [StringLength(1)]
        [RegularExpression("[FM]")]
        public string TipoPersona { get; set; } = null!;

        [Required]
        [MaxLength(13)]
        public string Rfc { get; set; } = null!;

        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string CorreoElectronico { get; set; } = null!;

        [Required]
        [MaxLength(13)]
        public string Telefono { get; set; } = null!;

        [MaxLength(60)]
        public string? ApellidoPaterno { get; set; }

        [MaxLength(60)]
        public string? ApellidoMaterno { get; set; }

        [MaxLength(60)]
        public string? Nombres { get; set; }

        [MaxLength(120)]
        public string? NombreCompleto { get; set; }

        public int? TipoSeguroId { get; set; }
        public int? OrigenClienteId { get; set; }
        public int? TipoCuentaId { get; set; }
        public int? TipoSectorId { get; set; }
        public int? RegimenFiscalId { get; set; }
        public int? BeneficiarioPreferenteId { get; set; }

        [MaxLength(120)]
        public string? Calle { get; set; }

        [MaxLength(10)]
        public string? NumeroInt { get; set; }

        [MaxLength(10)]
        public string? NumeroExt { get; set; }

        [Required]
        [MaxLength(50)]
        public string Pais { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string Estado { get; set; } = null!;

        [MaxLength(80)]
        public string? Municipio { get; set; }

        [MaxLength(60)]
        public string? Colonia { get; set; }

        [StringLength(5)]
        public string? Cp { get; set; }
    }
}
