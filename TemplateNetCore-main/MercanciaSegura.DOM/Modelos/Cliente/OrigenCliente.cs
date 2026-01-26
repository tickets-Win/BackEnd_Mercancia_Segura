using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercanciaSegura.DOM.Modelos.Cliente
{
    [Table("Origen_Cliente")]
    public class OrigenCliente
    {
        [Key]
        [Column("Origen_Cliente_ID")]
        public int OrigenClienteId { get; set; }

        [Column("Tipo")]
        [MaxLength(100)]
        public string? Tipo { get; set; }
    }
}
