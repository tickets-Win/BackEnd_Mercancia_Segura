using System;
using System.Collections.Generic;

namespace MercanciaSegura.RestAPI.Models.Cotizacion
{
    public class CotizacionResponse
    {
        public int CotizacionId { get; set; }

        public int? PolizaId { get; set; }
        public string NumeroPoliza { get; set; }

        public DateTime FechaCotizacion { get; set; }

        public int? ClienteId { get; set; }
        public string NombreCliente { get; set; }

        public DateTime? VigenciaDel { get; set; }

        public DateTime? VigenciaHasta { get; set; }

        public decimal? SumaAsegurada { get; set; }

        public decimal? GastosExpedicion { get; set; }

        public string MotivoCancelacion { get; set; }

        public DateTime? FechaCancelacion { get; set; }

        public List<CotizacionMercanciaResponse> CotizacionMercancia { get; set; }
        public List<CotizacionContenedorResponse> CotizacionContenedor { get; set; }
    }
}