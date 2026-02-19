using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercanciaSegura.DOM.Modelos.Poliza
{
    [Table("Poliza_Contenedor")]
    public class PolizaContenedor
    {
        [Key]
        [Column("Poliza_Contenedor_ID")]
        public int PolizaContenedorId { get; set; }

        [Column("Poliza_ID")]
        public int? PolizaId { get; set; }

        [Column("Nombre_Interno_Poliza")]
        [MaxLength(80)]
        public string? NombreInternoPoliza { get; set; }

        [Column("Por_Contenedor", TypeName = "decimal(10,2)")]
        public decimal? PorContenedor { get; set; }

        [Column("Ferrocarril", TypeName = "decimal(10,2)")]
        public decimal? Ferrocarril { get; set; }

        [Column("Terrestre", TypeName = "decimal(10,2)")]
        public decimal? Terrestre { get; set; }

        [Column("Cuota_Aplicable", TypeName = "decimal(10,2)")]
        public decimal? CuotaAplicable { get; set; }

        [Column("Danio_Material", TypeName = "decimal(10,2)")]
        public decimal? DanioMaterial { get; set; }

        [Column("Robo", TypeName = "decimal(10,2)")]
        public decimal? Robo { get; set; }

        [Column("Perdida_Parcial", TypeName = "decimal(10,2)")]
        public decimal? PerdidaParcial { get; set; }

        [Column("Perdida_Total", TypeName = "decimal(10,2)")]
        public decimal? PerdidaTotal { get; set; }

        [Column("Trayectos_Asegurados")]
        [MaxLength(250)]
        public string? TrayectosAsegurados { get; set; }

        [Column("Medio_Transporte")]
        [MaxLength(250)]
        public string? MedioTransporte { get; set; }

        [ForeignKey(nameof(PolizaId))]
        public Poliza? Poliza { get; set; }
        public ICollection<Cobertura>? Cobertura { get; set; } = new List<Cobertura>();

    }
}
