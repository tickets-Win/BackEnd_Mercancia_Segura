using System.Collections.Generic;

namespace MercanciaSegura.RestAPI.Models.Poliza
{
    public class PolizaContenedorResponse
    {
        public int PolizaContenedorId { get; set; }
        public int? PolizaId { get; set; }

        public string? NombreInternoPoliza { get; set; }

        public decimal? PorContenedor { get; set; }
        public decimal? Ferrocarril { get; set; }
        public decimal? Terrestre { get; set; }
        public decimal? CuotaAplicable { get; set; }
        public decimal? DanioMaterial { get; set; }
        public decimal? Robo { get; set; }
        public decimal? PerdidaParcial { get; set; }
        public decimal? PerdidaTotal { get; set; }

        public string? TrayectosAsegurados { get; set; }
        public string? MedioTransporte { get; set; }

        public List<CoberturaResponse>? Cobertura { get; set; }
    }

}
