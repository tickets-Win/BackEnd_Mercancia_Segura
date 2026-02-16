using System;

namespace MercanciaSegura.RestAPI.Models
{
    public class CotizacionResponse
    {
        public int CotizacionId { get; set; }

        public DateTime FechaCotizacion { get; set; }
        public int ClienteId { get; set; }
        public int PolizaId { get; set; }
        public int MonedaId { get; set; }

        public string CotizacionCliente { get; set; }
        public string Transito { get; set; }
        public DateTime VigenciaDel { get; set; }
        public DateTime VigenciaHasta { get; set; }

        public string Clasificacion { get; set; }
        public string SubClasificacion { get; set; }
        public string DescripcionMercancia { get; set; }

        public string Origen { get; set; }
        public string Destino { get; set; }
        public string TipoEmpaque { get; set; }

        public decimal SumaAsegurada { get; set; }
        public string BienesAsegurados { get; set; }

        public string TamanoYTipoContenedor { get; set; }
        public string NumeroContenedor { get; set; }

        public decimal CuotaAplicable { get; set; }
        public decimal CuotaMinima { get; set; }

        public decimal Subtotal { get; set; }
        public decimal IVA { get; set; }
        public decimal TotalSeguroMercancia { get; set; }
        public decimal TotalSeguroContenedor { get; set; }
        public decimal TotalPagar { get; set; }

        public decimal CuotaSecos { get; set; }
        public string TarifaSecos { get; set; }

        public decimal CuotaRefrigerados { get; set; }
        public string TarifaRefrigerados { get; set; }

        public decimal CuotaIsotanques { get; set; }
        public string TarifaIsotanques { get; set; }

        public decimal TotalTarifa { get; set; }

        public string Estatus { get; set; }
    }
}
