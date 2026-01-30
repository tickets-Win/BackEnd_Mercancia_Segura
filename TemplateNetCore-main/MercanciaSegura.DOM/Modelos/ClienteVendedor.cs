using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercanciaSegura.DOM.Modelos
{
    [Table("Cliente_Vendedor")]
    public class ClienteVendedor
    {
        [Key]
        [Column("Cliente_Vendedor_ID")]
        public int ClienteVendedorId { get; set; }

        [Column("Vendedor_ID")]
        public int? VendedorId { get; set; }

        [Column("Comision", TypeName = "decimal(10,2)")]
        public decimal? Comision { get; set; }

        [Column("Cliente_ID")]
        public int? ClienteId { get; set; }

        [ForeignKey(nameof(ClienteId))]
        public MercanciaSegura.DOM.Modelos.Cliente.Cliente? Cliente { get; set; }

        [ForeignKey(nameof(VendedorId))]
        public MercanciaSegura.DOM.Modelos.Vendedor.Vendedor? Vendedor { get; set; }

    }
}
