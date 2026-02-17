using System.Collections.Generic;

namespace MercanciaSegura.RestAPI.Models.Poliza
{
    public class PolizaCoberturaRequest
    {
        public string? Deducible { get; set; }

        public decimal? Prima { get; set; }

        public decimal? SumaAsegurada { get; set; }

        public List<CoberturaRequest>? Cobertura { get; set; }
    }
}
