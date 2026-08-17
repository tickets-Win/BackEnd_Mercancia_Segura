using System;

namespace MercanciaSegura.RestAPI.Models
{
    public class CertificadoResponse
    {
        public int CertificadoId { get; set; }

        public int CotizacionId { get; set; }

        public string? ClaveCertificado { get; set; }

        public string? Asegurado { get; set; }

        public DateTime FechaRegistro { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public decimal SumaAsegurada { get; set; }

        public int? TipoEstatusId { get; set; }

        // 🔹 Datos de la cotización que originó el certificado. El listado los
        //    necesita: el certificado por sí solo no tiene póliza ni tipo.
        public string? NombreEstatus { get; set; }

        public string? NombreCliente { get; set; }

        public string? NumeroPoliza { get; set; }

        public DateTime? FechaCotizacion { get; set; }

        /// <summary>"Mercancía" o "Contenedor", según el detalle de la cotización.</summary>
        public string? TipoCotizacion { get; set; }

        public decimal? Total { get; set; }
    }
}
