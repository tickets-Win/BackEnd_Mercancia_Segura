using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercanciaSegura.RestAPI.Models.Cotizacion
{
    public class CotizacionRequest
    {
        public int PolizaId { get; set; }

        public DateTime FechaCotizacion { get; set; }

        public int ClienteId { get; set; }

        public int? BeneficiarioPreferenteId { get; set; }

        public int MonedaId { get; set; }

        public DateTime? VigenciaDel { get; set; }

        public DateTime? VigenciaHasta { get; set; }

        public DateTime? FechaCancelacion { get; set; }

        public decimal? PrimaServicioDeAseguramiento { get; set; }

        public decimal? Subtotal { get; set; }

        public decimal? IVA { get; set; }

        public decimal? Total { get; set; }

        public decimal? GastosExpedicion { get; set; }

        public CotizacionMercanciaRequest? CotizacionMercancia { get; set; }
        public List<CotizacionContenedorRequest>? CotizacionContenedor { get; set; }
    }
}