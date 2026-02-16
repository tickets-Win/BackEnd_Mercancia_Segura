namespace MercanciaSegura.RestAPI.Models.Cliente
{
    public class ClienteCreditoResponse
    {
        public int ClienteCreditoId { get; set; }

        public int? DiasDeCredito { get; set; }

        public string? MetodoDePago { get; set; }

        public string? NumeroCuenta { get; set; }

        public decimal? LimiteDeCredito { get; set; }

        public string? DiasDePago { get; set; }

        public string? DiasDeRevision { get; set; }

        public decimal? Saldo { get; set; }
    }
}
