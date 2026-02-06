using System.ComponentModel.DataAnnotations;

namespace MercanciaSegura.RestAPI.Models.Poliza
{
    public class PolizaMercanciaRequest
    {
        [MaxLength(80)]
        public string? NombreInternoPoliza { get; set; }

        [MaxLength(20)]
        public string? FolioPoliza { get; set; }

        [MaxLength(250)]
        public string? BienesAsegurados { get; set; }

        [MaxLength(250)]
        public string? BienesExcluidos { get; set; }

        [MaxLength(250)]
        public string? BienesSujetosAConsulta { get; set; }

        public decimal? TerrestreAereo { get; set; }
        public decimal? Maritimo { get; set; }
        public decimal? PaqueteriaMensajeria { get; set; }

        [MaxLength(350)]
        public string? Deducibles { get; set; }

        [MaxLength(300)]
        public string? Compras { get; set; }

        [MaxLength(300)]
        public string? Ventas { get; set; }

        [MaxLength(300)]
        public string? Maquila { get; set; }

        [MaxLength(300)]
        public string? BienesUsados { get; set; }

        [MaxLength(300)]
        public string? EmbarqueFiliales { get; set; }

        [MaxLength(300)]
        public string? IndemnizacionOtros { get; set; }

        public decimal? Medicamentos { get; set; }
        public decimal? CobreAluminioAcero { get; set; }
        public decimal? MedicamentosControlados { get; set; }
        public decimal? EqContratistas { get; set; }

        public decimal? CuotaGeneralPoliza { get; set; }
    }
}
