using System;

namespace MercanciaSegura.RestAPI.Models
{
    /// <summary>
    /// Para dar de alta un certificado basta con la cotización: el API copia de
    /// ella al asegurado, la vigencia y la suma asegurada. Los demás campos son
    /// opcionales y solo se usan si vienen con valor.
    /// </summary>
    public class CertificadoRequest
    {
        public int CotizacionId { get; set; }

        public string? ClaveCertificado { get; set; }

        public string? Asegurado { get; set; }

        public DateTime? FechaRegistro { get; set; }

        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        public decimal? SumaAsegurada { get; set; }

        public int? TipoEstatusId { get; set; }
    }
}
