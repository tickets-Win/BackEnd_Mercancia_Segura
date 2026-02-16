namespace MercanciaSegura.RestAPI.Models.Cliente
{
    public class BeneficiarioPreferenteResponse
    {
        public int BeneficiarioPreferenteId { get; set; }

        public string? Nombre { get; set; }
        public string? Domicilio { get; set; }
        public string? Rfc { get; set; }
    }
}
