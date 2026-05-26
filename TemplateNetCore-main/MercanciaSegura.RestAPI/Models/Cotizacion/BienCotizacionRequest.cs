namespace MercanciaSegura.RestAPI.Models.Cotizacion
{
    public class BienCotizacionRequest
    {
        public int? TipoBienId { get; set; }

        public int? AdministracionBienId { get; set; }

        public string? Nombre { get; set; }
    }

}
