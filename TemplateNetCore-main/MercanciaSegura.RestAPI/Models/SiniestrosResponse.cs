using System;

namespace MercanciaSegura.RestAPI.Models
{
    public class SiniestrosResponse
    {
        public int SiniestroId { get; set; }

        public int CertificadoId { get; set; }

        public string? NReporte { get; set; }

        public DateTime FechaApertura { get; set; }

        public DateTime? FechaCierre { get; set; }

        public int? TipoSiniestroId { get; set; }

        public string? Mercancia { get; set; }

        public string? LugarDeSiniestro { get; set; }

        public decimal? SumaAsegurada { get; set; }

        public decimal? MontoDeReclamo { get; set; }

        public decimal? MontoDeIndemnizacion { get; set; }

        public string? TipoDeEventoId { get; set; }

        // 🔹 Datos enriquecidos (recomendado)
        public string? NombreTipoSiniestro { get; set; }
        public string? NumeroCertificado { get; set; }
    }
}
