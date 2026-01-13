using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercanciaSegura.DOM.Modelos
{
    [Table("Tipo_Vendedor")]
    public class TipoVendedor
    {
        [Key]
        [Column("Tipo_Vendedor_ID")]
        public int TipoVendedorId { get; set; }

        [Column("Tipo")]
        [MaxLength(20)]
        public string? Tipo { get; set; }
    }
}
