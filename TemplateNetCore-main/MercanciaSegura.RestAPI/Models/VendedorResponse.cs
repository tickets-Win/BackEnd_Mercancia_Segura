using System;
using System.ComponentModel.DataAnnotations;

namespace MercanciaSegura.RestAPI.Models
{
    public class VendedorResponse
    {
        public int VendedorId { get; set; }

        [MaxLength(60)]
        public string? ApellidoPaterno { get; set; }

        [MaxLength(60)]
        public string? ApellidoMaterno { get; set; }

        [MaxLength(60)]
        public string? Nombres { get; set; }

        [MaxLength(200)]
        public string? NombreCompleto { get; set; }

        // Llaves foráneas
        public int? TipoPersonaId { get; set; }
        public int? TipoVendedorId { get; set; }

        public bool Estatus { get; set; }

        [MaxLength(10)]
        public string? Clave { get; set; }

        [MaxLength(13)]
        public string? Rfc { get; set; }

        [MaxLength(300)]
        public string? Domicilio { get; set; }

        [StringLength(5)]
        public string? Cp { get; set; }

        [MaxLength(60)]
        public string? Colonia { get; set; }

        [MaxLength(20)]
        public string? Estado { get; set; }

        [StringLength(1)]
        public string? Genero { get; set; }

        [MaxLength(13)]
        public string? Telefono { get; set; }

        [MaxLength(200)]
        public string? CorreoElectronico { get; set; }

        [MaxLength(200)]
        public string? Observaciones { get; set; }

        public decimal? Comision { get; set; }

        public DateTime FechaRegistro { get; set; }

        public DateTime? FechaBaja { get; set; }
    }
}
