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

        [Column("Deducibles", TypeName = "nvarchar(max)")]
        public string? Deducibles { get; set; }

        [Column("Compras", TypeName = "nvarchar(max)")]
        public string? Compras { get; set; }

        [Column("Ventas", TypeName = "nvarchar(max)")]
        public string? Ventas { get; set; }

        [Column("Maquila", TypeName = "nvarchar(max)")]
        public string? Maquila { get; set; }

        [Column("Bienes_Usados", TypeName = "nvarchar(max)")]
        public string? BienesUsados { get; set; }

        [Column("Embarque_Filiales", TypeName = "nvarchar(max)")]
        public string? EmbarqueFiliales { get; set; }

        [Column("Indemnizacion_Otros", TypeName = "nvarchar(max)")]
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

        [Column("Prima_Neta", TypeName = "decimal(10,2)")]
        public decimal? PrimaNeta { get; set; }

        [Column("Derecho_Poliza", TypeName = "decimal(10,2)")]
        public decimal? DerechoPoliza { get; set; }

        [Column("Otro_Prima", TypeName = "decimal(10,2)")]
        public decimal? OtroPrima { get; set; }

        [Column("IVA", TypeName = "decimal(10,2)")]
        public decimal? IVA { get; set; }

        [Column("Prima_Total", TypeName = "decimal(10,2)")]
        public decimal? PrimaTotal { get; set; }


        // 🔗 Relación (si existe la tabla Poliza)
        [ForeignKey(nameof(PolizaId))]
        public Poliza? Poliza { get; set; }
        public ICollection<RiesgoCubierto> RiesgoCubierto { get; set; } = new List<RiesgoCubierto>();
    }
}
