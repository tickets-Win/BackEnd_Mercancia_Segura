namespace MercanciaSegura.RestAPI.Models.Cotizacion
{
    public class CotizacionContenedorRequest
    {

        public string? TamanoTipoContendor { get; set; }
        public string? NumeroContenedor { get; set; }

        public decimal? OpcionCuota { get; set; }
        public decimal? Tarifa { get; set; }
        public decimal? TotalTarifa { get; set; }

        public decimal? IVA { get; set; }
        public decimal? Total { get; set; }
        public decimal? TotalSeguroContenedor { get; set; }
    }
}