namespace MercanciaSegura.RestAPI.Models.Cotizacion
{
    public class CotizacionMercanciaResponse
    {
        public int CotizacionMercanciaId { get; set; }

        public int CotizacionId { get; set; }

        public string? CotizacionCliente { get; set; }

        public string? Transito { get; set; }

        public int? ClasificacionId { get; set; }

        public string? ClasificacionNombre { get; set; }

        public string? SubClasificacion { get; set; }

        public string? DescripcionMercancia { get; set; }

        public string? TipoEmpaque { get; set; }

        public string? Origen { get; set; }

        public string? Destino { get; set; }

        public string? MedioDeConduccion { get; set; }

        public string? MedioDeTransporte { get; set; }

        public string? Observaciones { get; set; }

        public string? MedidasDeSeguridadAdicionales { get; set; }

        public string? Deducibles { get; set; }

        public int? MonedaCuotaAplicableId { get; set; }

        public decimal? CuotaAplicable { get; set; }

        public int? MonedaCuotaMinimaId { get; set; }

        public decimal? CuotaMinima { get; set; }

        public decimal? TipoCambioCotizar { get; set; }

        public int? MonedaCotizarId { get; set; }

        public decimal? SumaAsegurada { get; set; }
    }
}