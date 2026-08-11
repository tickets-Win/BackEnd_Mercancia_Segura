using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercanciaSegura.DOM.Modelos.Cotizacion
{
    [Table("Transito")]
    public class Transito
    {
        [Key]
        [Column("Transito_ID")]
        public int Transito_Id { get; set; }

        [Column("Nombre")]
        [MaxLength(100)]
        public string? Nombre { get; set; }
    }
}
