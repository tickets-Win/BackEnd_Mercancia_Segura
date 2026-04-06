using System;

namespace MercanciaSegura.RestAPI.Models
{
    public class EndososResponse
    {
        public int EndosoId { get; set; }

        public int TipoEndosoId { get; set; }

        public string? NumeroEndoso { get; set; }

        public int CertificadoId { get; set; }

        public DateTime FechaElaboracion { get; set; }

        public string? Agente { get; set; }

        public string? RFC { get; set; }

        public string? Oficina { get; set; }

        public string? BeneficiarioPreferente { get; set; }

        public int? MonedaId { get; set; }

        public decimal? IVA { get; set; }

        public decimal? TotalAPagar { get; set; }

        public string? Descripcion { get; set; }

        // 🔹 Datos enriquecidos (recomendado)
        public string? NombreTipoEndoso { get; set; }
        public string? NumeroCertificado { get; set; }
        public string? Moneda { get; set; }
    }
}
