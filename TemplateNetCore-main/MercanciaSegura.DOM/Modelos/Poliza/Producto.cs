using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercanciaSegura.DOM.Modelos.Poliza
{
    [Table("Producto")]
    public class Producto
    {
        [Key]
        [Column("Producto_ID")]
        public int ProductoId { get; set; }

        [Column("Nombre")]
        [MaxLength(100)]
        public string? Nombre { get; set; }

        [Column("Pantalla")]
        [MaxLength(100)]
        public string? Pantalla { get; set; }
    }
}
