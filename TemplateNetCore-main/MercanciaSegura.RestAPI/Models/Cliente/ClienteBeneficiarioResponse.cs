namespace MercanciaSegura.RestAPI.Models.Cliente
{
    public class ClienteBeneficiarioResponse
    {
        public int ClienteBeneficiarioId { get; set; }
        public int? ClienteId { get; set; }
        public string NombreCliente { get; set; }
        public int? BeneficiarioPreferenteId { get; set; }
        public string NombreCompletoBP { get; set; }
        public string ClaveBP { get; set; }
        public string RFCBP { get; set; }
        public string RFCGenericoBP { get; set; }
        public string PaisBP { get; set; }
    }
}
