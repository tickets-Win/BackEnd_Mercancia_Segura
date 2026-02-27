using System;

namespace MercanciaSegura.RestAPI.Models.Cliente
{
    public class BeneficiarioPreferenteResponse
    {
        public int BeneficiarioPreferenteId { get; set; }

        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string RFC { get; set; }
        public string Clave { get; set; }

        public int? TipoPersonaId { get; set; }
        public string? tipo { get; set; }
        public int? RfcGenericoId { get; set; }
        public string? rfcGenerico { get; set; }
        public string Calle { get; set; }
        public string NumeroInt { get; set; }
        public string NumeroExt { get; set; }
        public string Poblacion { get; set; }
        public string Colonia { get; set; }
        public string Cp { get; set; }
        public string Pais { get; set; }
        public string Nacionalidad { get; set; }

        public DateTime? FechaActualizacion { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}