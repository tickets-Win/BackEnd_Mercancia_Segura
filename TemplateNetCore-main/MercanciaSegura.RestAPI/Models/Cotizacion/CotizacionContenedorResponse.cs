namespace MercanciaSegura.RestAPI.Models.Cotizacion
{
    public class CotizacionContenedorResponse
    {
        public int CotizacionContenedorId { get; set; }

        public int CotizacionId { get; set; }

        public int? TamanioContendorId { get; set; }
        public string? TamanioContenedorNombre { get; set; }

        public int? TipoContenedorId { get; set; }
        public string? TipoContenedorNombre { get; set; }

        public string? NumeroContenedor { get; set; }

        public decimal? Cuota { get; set; }

        public decimal? LR { get; set; }

        public string? Referencia { get; set; }

        public decimal? TC { get; set; }

        public decimal? PrimaUnitariaUSD { get; set; }

        public decimal? PrimaUnitariaMXN { get; set; }

        public decimal? Total { get; set; }
    }
}