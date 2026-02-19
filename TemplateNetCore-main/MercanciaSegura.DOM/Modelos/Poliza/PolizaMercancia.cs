using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercanciaSegura.DOM.Modelos.Poliza
{
    [Table("Poliza_Mercancia")]
    public class PolizaMercancia
    {
        [Key]
        [Column("Poliza_Mercancia_ID")]
        public int PolizaMercanciaId { get; set; }

        [Column("Poliza_ID")]
        public int? PolizaId { get; set; }

        [Column("Administracion_Bien_ID")]
        public int AdministracionBienId { get; set; }

        [Column("Nombre_Interno_Poliza")]
        [MaxLength(80)]
        public string? NombreInternoPoliza { get; set; }

        [Column("Terrestre_Aereo", TypeName = "decimal(10,2)")]
        public decimal? TerrestreAereo { get; set; }

        [Column("Maritimo", TypeName = "decimal(10,2)")]
        public decimal? Maritimo { get; set; }

        [Column("Paqueteria_Mensajeria", TypeName = "decimal(10,2)")]
        public decimal? PaqueteriaMensajeria { get; set; }

        [Column("Deducibles")]
        [MaxLength(350)]
        public string? Deducibles { get; set; }

        [Column("Compras")]
        [MaxLength(300)]
        public string? Compras { get; set; }

        [Column("Ventas")]
        [MaxLength(300)]
        public string? Ventas { get; set; }

        [Column("Maquila")]
        [MaxLength(300)]
        public string? Maquila { get; set; }

        [Column("Bienes_Usados")]
        [MaxLength(300)]
        public string? BienesUsados { get; set; }

        [Column("Embarque_Filiales")]
        [MaxLength(300)]
        public string? EmbarqueFiliales { get; set; }

        [Column("Indemnizacion_Otros")]
        [MaxLength(300)]
        public string? IndemnizacionOtros { get; set; }

        [Column("Medicamentos", TypeName = "decimal(10,2)")]
        public decimal? Medicamentos { get; set; }

        [Column("Cobre_Aluminio_Acero", TypeName = "decimal(10,2)")]
        public decimal? CobreAluminioAcero { get; set; }

        [Column("Medicamentos_Controlados", TypeName = "decimal(10,2)")]
        public decimal? MedicamentosControlados { get; set; }

        [Column("EQ_Contratistas", TypeName = "decimal(10,2)")]
        public decimal? EqContratistas { get; set; }

        [Column("Cuota_General_Poliza", TypeName = "decimal(10,2)")]
        public decimal? CuotaGeneralPoliza { get; set; }

        // 🔗 Relación (si existe la tabla Poliza)
        [ForeignKey(nameof(PolizaId))]
        public Poliza? Poliza { get; set; }
        public ICollection<RiesgoCubierto> RiesgoCubierto { get; set; } = new List<RiesgoCubierto>();
    }
}
