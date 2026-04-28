using System;
using System.Collections.Generic;

namespace MercanciaSegura.RestAPI.Models.Cotizacion
{
    public class CotizacionResponse
    {
        public int CotizacionId { get; set; }

        public int? PolizaId { get; set; }
        public string? NumeroPoliza { get; set; }

        public DateTime FechaCotizacion { get; set; }

        public int? ClienteId { get; set; }
        public string? NombreCliente { get; set; }

        public int? BeneficiarioPreferenteId { get; set; }
        public string? NombreBeneficiario { get; set; }

        public int? MonedaId { get; set; }
        public string? MonedaNombre { get; set; }

        public DateTime? VigenciaDel { get; set; }

        public DateTime? VigenciaHasta { get; set; }

        public DateTime? FechaCancelacion { get; set; }

        public decimal? PrimaServicioDeAseguramiento { get; set; }

        public decimal? Subtotal { get; set; }

        public decimal? IVA { get; set; }

        public decimal? Total { get; set; }

        public decimal? GastosExpedicion { get; set; }

        public CotizacionMercanciaResponse? CotizacionMercancia { get; set; }

        public List<CotizacionContenedorResponse>? CotizacionContenedor { get; set; }
    }
}