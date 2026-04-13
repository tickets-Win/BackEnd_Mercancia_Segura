namespace MercanciaSegura.RestAPI.Models.Cotizacion
{
    public class CotizacionMercanciaResponse
    {
        public int CotizacionMercanciaId { get; set; }

        public int CotizacionId { get; set; }

        public string? CotizacionCliente { get; set; }

        public string? Transito { get; set; }

        public string? Clasificacion { get; set; }

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

        public decimal? CuotaAplicable { get; set; }

        public decimal? CuotaMinima { get; set; }

        public decimal? TipoCambioCotizar { get; set; }

        public decimal? PrimaServicioDeAseguramiento { get; set; }

        public decimal? Subtotal { get; set; }

        public decimal? IVA { get; set; }

        public decimal? TotalSeguroMercancia { get; set; }

        public decimal? Total { get; set; }
    }
}