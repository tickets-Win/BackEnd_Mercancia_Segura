using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercanciaSegura.DOM.Modelos.Poliza
{
    [Table("Poliza_Contenedor")]
    public class PolizaContenedor
    {
        [Key]
        [Column("Poliza_ID")]
        public int PolizaId { get; set; }

        [Column("Bienes_Asegurados")]
        [MaxLength(250)]
        public string? BienesAsegurados { get; set; }

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

        [Column("Medio_Transporte", TypeName = "decimal(10,2)")]
        public decimal? MedioTransporte { get; set; }

        // 🔗 Relación 1 a 1 con Poliza
        [ForeignKey(nameof(PolizaId))]
        public Poliza? Poliza { get; set; }
    }
}
