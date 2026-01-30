namespace MercanciaSegura.RestAPI.Models
{
    public class ClienteVendedorRequest
    {
        public int VendedorId { get; set; }
        public decimal? Comision { get; set; }
    }
}
