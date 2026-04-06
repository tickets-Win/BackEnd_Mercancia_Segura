using System;

namespace MercanciaSegura.RestAPI.Models
{
    public class CertificadoResponse
    {
        public int CertificadoId { get; set; }

        public int CotizacionId { get; set; }

        public DateTime FechaCertificado { get; set; }

        // 🔹 Opcional (recomendado)
        public string NumeroCotizacion { get; set; }
    }
}
