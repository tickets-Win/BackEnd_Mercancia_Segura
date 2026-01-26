using System;
using System.ComponentModel.DataAnnotations;
namespace MercanciaSegura.RestAPI.Models
{
    public class ClienteRequest
    {
        // Información fiscal y contacto
        [Required, MaxLength(13)]
        public string? Rfc { get; set; }

        public int? RfcGenericoId { get; set; }

        [MaxLength(13)]
        public string? Telefono { get; set; }

        [EmailAddress, MaxLength(200)]
        public string? CorreoElectronico { get; set; }

        [MaxLength(30)]
        public string? Nacionalidad { get; set; }

        // Datos personales
        [MaxLength(60)]
        public string? ApellidoPaterno { get; set; }

        [MaxLength(60)]
        public string? ApellidoMaterno { get; set; }

        [MaxLength(60)]
        public string? Nombres { get; set; }

        [MaxLength(120)]
        public string? NombreCompleto { get; set; }

        // Dirección
        [MaxLength(120)]
        public string? Calle { get; set; }

        [MaxLength(10)]
        public string? NumeroInt { get; set; }

        [MaxLength(10)]
        public string? NumeroExt { get; set; }

        [MaxLength(50)]
        public string? Pais { get; set; }

        [MaxLength(80)]
        public string? Municipio { get; set; }

        [MaxLength(80)]
        public string? Poblacion { get; set; }

        [MaxLength(60)]
        public string? Colonia { get; set; }

        [MaxLength(20)]
        public string? Estado { get; set; }

        [StringLength(5)]
        public string? Cp { get; set; }

        // Llaves foráneas
        public int? TipoSeguroId { get; set; }
        public int? OrigenClienteId { get; set; }
        public int? TipoCuentaId { get; set; }
        public int? TipoSectorId { get; set; }
        public int? RegimenFiscalId { get; set; }
        public int? TipoPersonaId { get; set; }
        public int? EstatusId { get; set; }

        // Cuotas
        public decimal? CuotaMinimaInternacional { get; set; }
        public decimal? CuotaMinimaNacional { get; set; }
        public decimal? CuotaAplicableInternacional { get; set; }
        public decimal? CuotaAplicableNacional { get; set; }
        public BeneficiarioPreferenteRequest? Beneficiario { get; set; }
    }
}
