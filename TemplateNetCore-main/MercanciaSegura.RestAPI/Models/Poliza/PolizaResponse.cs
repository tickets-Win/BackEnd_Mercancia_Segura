using System;
using System.Collections.Generic;

namespace MercanciaSegura.RestAPI.Models.Poliza
{
    public class PolizaResponse
    {
        public int PolizaId { get; set; }

        public int? ProductoId { get; set; }
        public string? NombreProducto { get; set; }

        public int? ContratanteId { get; set; }
        public string? NombreContratante { get; set; }

        public int? AseguradoraId { get; set; }
        public string? NombreAseguradora { get; set; }

        public int? SubRamoId { get; set; }
        public string? NombreSubRamo { get; set; }

        public int? MonedaId { get; set; }
        public string? NombreMoneda { get; set; }

        public int? FormaPagoId { get; set; }
        public string? NombreFormaPago { get; set; }

        public int? EstatusPolizaId { get; set; }
        public string? NombreEstatusPoliza { get; set; }

        public string? NumeroPoliza { get; set; }
        public string? TipoPoliza { get; set; }
        public string? ClaveAgente { get; set; }
        public string? FolioPoliza { get; set; }

        public DateTime? VigenciaDel { get; set; }
        public DateTime? VigenciaHasta { get; set; }

        public string? OtrosPoliza { get; set; }

        public decimal? PrimaNeta { get; set; }
        public decimal? DerechoPoliza { get; set; }
        public decimal? Otros { get; set; }
        public decimal? IVA { get; set; }
        public decimal? PrimaTotal { get; set; }

        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public DateTime? FechaBaja { get; set; }

        public PolizaContenedorResponse? PolizaContenedor { get; set; }
        public List<PolizaMercanciaResponse>? PolizaMercancia { get; set; }
        public List<BienResponse>? Bien { get; set; }
    }

}
