namespace MercanciaSegura.RestAPI.Models.Poliza
{
    public class PolizaCoberturaRequest
    {
        public int PolizaId { get; set; }

        public string? Deducible { get; set; }

        public decimal? Prima { get; set; }

        public decimal? SumaAsegurada { get; set; }
    }
}
