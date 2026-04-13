using System;

namespace MercanciaSegura.RestAPI.Models
{
    public class CertificadoRequest
    {
        public int CotizacionId { get; set; }

        public DateTime FechaCertificado { get; set; }
    }
}
