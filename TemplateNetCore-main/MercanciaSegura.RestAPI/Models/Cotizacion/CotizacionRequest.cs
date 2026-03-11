using System;
using System.Collections.Generic;
using MercanciaSegura.DOM.Modelos;

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

        public CotizacionMercanciaRequest CotizacionMercancia { get; set; } = new();
        public CotizacionContenedorRequest CotizacionContenedor { get; set; } = new();
    }
}

