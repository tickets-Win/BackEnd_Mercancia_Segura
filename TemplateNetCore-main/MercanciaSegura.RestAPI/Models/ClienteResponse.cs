using System;

namespace MercanciaSegura.RestAPI.Models
{
    public class ClienteResponse
    {
        public int ClienteId { get; set; }
        public string TipoPersona { get; set; } = string.Empty;
        public string Rfc { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string CorreoElectronico { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
    }
}
