using System.Collections.Generic;

namespace MercanciaSegura.RestAPI.Models.Poliza
{
    public class PolizaCoberturaResponse
    {
        public int PolizaCoberturaId { get; set; }

        public int PolizaId { get; set; }

        public string? Deducible { get; set; }

        public decimal? Prima { get; set; }

        public decimal? SumaAsegurada { get; set; }


        public List<CoberturaResponse> Cobertura { get; set; } = new();
    }
}
