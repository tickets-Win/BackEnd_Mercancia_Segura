using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercanciaSegura.DOM.Modelos.Poliza
{
    [Table("Riesgo")]
    public class Riesgo
    {
        [Key]
        [Column("Riesgo_ID")]
        public int RiesgoId { get; set; }

        [Column("Producto_ID")]
        public int ProductoId { get; set; }

        [Column("Nombre")]
        [MaxLength(100)]
        public string? Nombre { get; set; }

        [ForeignKey(nameof(ProductoId))]
        public Producto? Producto { get; set; }
    }
}
