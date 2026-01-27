using System;
using System.ComponentModel.DataAnnotations;

namespace MercanciaSegura.RestAPI.Models
{
    public class VendedorRequest
    {
        [MaxLength(60)]
        public string? ApellidoPaterno { get; set; }

        [MaxLength(60)]
        public string? ApellidoMaterno { get; set; }

        [MaxLength(60)]
        public string? Nombres { get; set; }

        [MaxLength(200)]
        public string? NombreCompleto { get; set; }

        // FK
        public int? TipoPersonaId { get; set; }

        // FK
        public int? TipoVendedorId { get; set; }

        public bool Estatus { get; set; } = true;

        [MaxLength(10)]
        public string? Clave { get; set; }

        [MaxLength(13)]
        public string? Rfc { get; set; }

        [MaxLength(300)]
        public string? Domicilio { get; set; }

        [StringLength(5)]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "CP inválido")]
        public string? Cp { get; set; }

        [MaxLength(60)]
        public string? Colonia { get; set; }

        [MaxLength(20)]
        public string? Estado { get; set; }

        [StringLength(1)]
        [RegularExpression("M|F", ErrorMessage = "Genero debe ser 'M' o 'F'")]
        public string? Genero { get; set; }

        [MaxLength(13)]
        public string? Telefono { get; set; }

        [MaxLength(200)]
        [EmailAddress]
        public string? CorreoElectronico { get; set; }

        [MaxLength(200)]
        public string? Observaciones { get; set; }

        [Range(0, 100)]
        public decimal? Comision { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
