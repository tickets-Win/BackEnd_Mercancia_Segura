using System;
using System.Collections.Generic;

namespace MercanciaSegura.RestAPI.Models.Cotizacion
{
    public class CotizacionRequest
    {
        public int? PolizaId { get; set; }

        public DateTime FechaCotizacion { get; set; }

        public int? ClienteId { get; set; }

        public DateTime? VigenciaDel { get; set; }

        public DateTime? VigenciaHasta { get; set; }

        public decimal? SumaAsegurada { get; set; }

        public decimal? GastosExpedicion { get; set; }

        public string MotivoCancelacion { get; set; }

        public DateTime? FechaCancelacion { get; set; }

        public List<CotizacionMercanciaRequest> CotizacionMercancia { get; set; }

        public List<CotizacionContenedorRequest> CotizacionContenedor { get; set; }
    }
}

