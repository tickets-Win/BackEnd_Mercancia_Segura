using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MercanciaSegura.RestAPI.Models.Poliza
{
    public class PolizaContenedorRequest
    {
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

        public List<CoberturaRequest>? Cobertura { get; set; }
    }

}
