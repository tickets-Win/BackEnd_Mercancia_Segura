using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MercanciaSegura.DOM.Modelos.Cliente;

namespace MercanciaSegura.DOM.Modelos.Poliza
{
    [Table("Poliza")]
    public class Poliza
    {
        [Key]
        [Column("Poliza_ID")]
        public int PolizaId { get; set; }

        [Column("Producto_ID")]
        public int? ProductoId { get; set; }

        [Column("Contratante_ID")]
        public int? ContratanteId { get; set; }

        [Column("Aseguradora_ID")]
        public int? AseguradoraId { get; set; }

        [Column("SubRamo_ID")]
        public int? SubRamoId { get; set; }

        [Column("Moneda_ID")]
        public int? MonedaId { get; set; }

        [Column("Forma_Pago_ID")]
        public int? FormaPagoId { get; set; }

        [Column("Estatus_Poliza_ID")]
        public int? EstatusPolizaId { get; set; }

        [Column("Numero_Poliza")]
        [MaxLength(30)]
        public string? NumeroPoliza { get; set; }

        [Column("Tipo_Poliza")]
        [MaxLength(150)]
        public string? TipoPoliza { get; set; }

        [Column("Clave_Agente")]
        [MaxLength(50)]
        public string? ClaveAgente { get; set; }

        [Column("Folio_Poliza")]
        [MaxLength(20)]
        public string? FolioPoliza { get; set; }


        [Column("Vigencia_Del")]
        public DateTime? VigenciaDel { get; set; }

        [Column("Vigencia_Hasta")]
        public DateTime? VigenciaHasta { get; set; }

        [Column("Otros_Poliza")]
        [MaxLength(100)]
        public string? OtrosPoliza { get; set; }

        [Column("Prima_Neta", TypeName = "decimal(10,2)")]
        public decimal? PrimaNeta { get; set; }

        [Column("Derecho_Poliza", TypeName = "decimal(10,2)")]
        public decimal? DerechoPoliza { get; set; }

        [Column("Otros", TypeName = "decimal(10,2)")]
        public decimal? Otros { get; set; }

        [Column("IVA", TypeName = "decimal(10,2)")]
        public decimal? IVA { get; set; }

        [Column("Prima_Total", TypeName = "decimal(10,2)")]
        public decimal? PrimaTotal { get; set; }

        [Column("Fecha_Actualizacion")]
        public DateTime? FechaActualizacion { get; set; }

        [Column("Fecha_Registro")]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [Column("Fecha_Baja")]
        public DateTime? FechaBaja { get; set; }

        [ForeignKey(nameof(ProductoId))]
        public Producto? Producto { get; set; }

        [ForeignKey(nameof(ContratanteId))]
        public Contratante? Contratante { get; set; }

        [ForeignKey(nameof(AseguradoraId))]
        public Aseguradora? Aseguradora { get; set; }

        [ForeignKey(nameof(SubRamoId))]
        public SubRamo? SubRamo { get; set; }

        [ForeignKey(nameof(MonedaId))]
        public Moneda? Moneda { get; set; }

        [ForeignKey(nameof(FormaPagoId))]
        public FormaPago? FormaPago { get; set; }

        [ForeignKey(nameof(EstatusPolizaId))]
        public EstatusPoliza? EstatusPoliza { get; set; }

        public PolizaContenedor? PolizaContenedor { get; set; }
        public ICollection<PolizaMercancia> PolizaMercancia { get; set; } = new List<PolizaMercancia>();
        public ICollection<Bien> Bien { get; set; } = new List<Bien>();


    }
}
