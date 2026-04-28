using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercanciaSegura.DOM.Modelos.Cotizacion
{
    [Table("Tamanio_Contenedor")]
    public class TamanioContenedor
    {
        [Key]
        [Column("Tamanio_Contenedor_ID")]
        public int TamanioContenedorId { get; set; }

        [Column("Nombre")]
        [MaxLength(100)]
        public string? Nombre { get; set; }

    }
}

