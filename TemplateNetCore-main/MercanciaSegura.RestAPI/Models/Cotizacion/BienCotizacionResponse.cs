namespace MercanciaSegura.RestAPI.Models.Cotizacion
{
    public class BienCotizacionResponse
    {
        public int BienCotizacionId { get; set; }

        public int? CotizacionId { get; set; }

        public int? AdministracionBienId { get; set; }

        public int? TipoBienId { get; set; }
        public string? NombreTipoBien { get; set; }

        public string? Nombre { get; set; }
    }
}
