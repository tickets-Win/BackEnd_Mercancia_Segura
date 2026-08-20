using System;

namespace MercanciaSegura.RestAPI.Models
{
    public class EndososRequest
    {
        public int TipoEndosoId { get; set; }

        public string? NumeroEndoso { get; set; }

        public int CertificadoId { get; set; }

        public DateTime FechaElaboracion { get; set; }

        public string? Agente { get; set; }

        public string? RFC { get; set; }

        public string? Oficina { get; set; }

        public string? BeneficiarioPreferente { get; set; }

        public int? MonedaId { get; set; }

        public DateTime? VigenciaDel { get; set; }

        public DateTime? VigenciaHasta { get; set; }

        public decimal? SumaAsegurada { get; set; }

        public decimal? PrimaServicioDeAseguramiento { get; set; }

        public decimal? IVA { get; set; }

        public decimal? TotalAPagar { get; set; }

        public string? Descripcion { get; set; }
    }
}
