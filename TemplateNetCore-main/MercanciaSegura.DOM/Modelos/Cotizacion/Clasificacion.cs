using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercanciaSegura.DOM.Modelos.Cotizacion
{
    [Table("Clasificacion")]
    public class Clasificacion
    {
        [Key]
        [Column("Clasificacion_ID")]
        public int Clasificacion_Id { get; set; }

        [Column("Nombre")]
        [MaxLength(100)]
        public string? Nombre { get; set; }
    }
}
