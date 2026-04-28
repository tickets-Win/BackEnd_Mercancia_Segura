namespace MercanciaSegura.RestAPI.Models.Cotizacion
{
    public class CotizacionContenedorRequest
    {
        public int? TamanioContendorId { get; set; }

        public int? TipoContenedorId { get; set; }

        public int? NumeroContenedor { get; set; }

        public decimal? Cuota { get; set; }

        public decimal? LR { get; set; }

        public string? Referencia { get; set; }

        public decimal? TC { get; set; }

        public decimal? PrimaUnitariaUSD { get; set; }

        public decimal? PrimaUnitariaMXN { get; set; }

        public decimal? Total { get; set; }
    }
}