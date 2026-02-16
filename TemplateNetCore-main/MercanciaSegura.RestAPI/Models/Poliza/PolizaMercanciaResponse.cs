using System;

namespace MercanciaSegura.RestAPI.Models.Poliza
{
    public class PolizaMercanciaResponse
    {
        public int PolizaId { get; set; }

        public string? NombreInternoPoliza { get; set; }
        public string? FolioPoliza { get; set; }

        public string? BienesAsegurados { get; set; }
        public string? BienesExcluidos { get; set; }
        public string? BienesSujetosAConsulta { get; set; }

        public decimal? TerrestreAereo { get; set; }
        public decimal? Maritimo { get; set; }
        public decimal? PaqueteriaMensajeria { get; set; }

        public string? Deducibles { get; set; }
        public string? Compras { get; set; }
        public string? Ventas { get; set; }
        public string? Maquila { get; set; }
        public string? BienesUsados { get; set; }
        public string? EmbarqueFiliales { get; set; }
        public string? IndemnizacionOtros { get; set; }

        public decimal? Medicamentos { get; set; }
        public decimal? CobreAluminioAcero { get; set; }
        public decimal? MedicamentosControlados { get; set; }
        public decimal? EqContratistas { get; set; }

        public decimal? CuotaGeneralPoliza { get; set; }
    }
}
