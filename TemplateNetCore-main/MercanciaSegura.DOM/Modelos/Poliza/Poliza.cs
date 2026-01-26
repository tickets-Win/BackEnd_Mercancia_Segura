using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MercanciaSegura.DOM.Modelos.Poliza.Enum;

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

        [Column("Estatus")]
        [MaxLength(20)]
        public string? Estatus { get; set; }

        [Column("Tipo_Poliza")]
        [MaxLength(50)]
        public TipoPolizaEnum TipoPoliza { get; set; }


        [Column("Numero_Poliza")]
        [MaxLength(30)]
        public string? NumeroPoliza { get; set; }

        [Column("Vigencia_Del")]
        public DateTime? VigenciaDel { get; set; }

        [Column("Vigencia_Hasta")]
        public DateTime? VigenciaHasta { get; set; }

        [Column("Otros_Poliza")]
        [MaxLength(100)]
        public string? OtrosPoliza { get; set; }

        [Column("Prima_Neta", TypeName = "decimal(18,2)")]
        public decimal? PrimaNeta { get; set; }

        [Column("Derecho_Poliza", TypeName = "decimal(18,2)")]
        public decimal? DerechoPoliza { get; set; }

        [Column("Otros", TypeName = "decimal(18,2)")]
        public decimal? Otros { get; set; }

        [Column("IVA", TypeName = "decimal(18,2)")]
        public decimal? IVA { get; set; }

        [Column("Prima_Total", TypeName = "decimal(18,2)")]
        public decimal? PrimaTotal { get; set; }
    }
}
