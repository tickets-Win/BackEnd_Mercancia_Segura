using System;

namespace MercanciaSegura.RestAPI.Models
{
    public class CotizacionRequest
    {
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
        public string Observaciones { get; set; }

        public string Origen { get; set; }
        public string Destino { get; set; }
        public string TipoEmpaque { get; set; }
        public string MedioDeTransporte { get; set; }
        public string MedioDeConduccion { get; set; }

        public decimal SumaAsegurada { get; set; }
        public string BienesAsegurados { get; set; }

        public string TamanoYTipoContenedor { get; set; }
        public string NumeroContenedor { get; set; }

        public decimal OpcionCuota { get; set; }
        public string MedidasDeSeguridadAdicionales { get; set; }
        public decimal Deducibles { get; set; }

        public decimal CuotaAplicable { get; set; }
        public decimal CuotaMinima { get; set; }

        public string MonedaParaCotizar { get; set; }
        public decimal TipoCambioCotizar { get; set; }

        public decimal PrimaServicioDeAseguramiento { get; set; }
        public decimal GastosDeExpedicion { get; set; }
    }
}

