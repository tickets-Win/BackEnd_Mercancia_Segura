namespace MercanciaSegura.RestAPI.Models.Cliente
{
    public class ClienteBeneficiarioResponse
    {
        public int ClienteBeneficiarioId { get; set; }
        public int? ClienteId { get; set; }
        public string NombreCliente { get; set; }
        public int? BeneficiarioPreferenteId { get; set; }
        public string NombreBeneficiarioPreferente { get; set; }
    }
}
