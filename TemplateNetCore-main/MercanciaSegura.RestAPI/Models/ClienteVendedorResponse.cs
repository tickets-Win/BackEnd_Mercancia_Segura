namespace MercanciaSegura.RestAPI.Models
{
    public class ClienteVendedorResponse
    {
        public int ClienteVendedorId { get; set; }
        public int? VendedorId { get; set; }
        public decimal? Comision { get; set; }

        // Datos descriptivos del vendedor (opcional pero recomendado)
        public string NombreVendedor { get; set; }
    }
}
