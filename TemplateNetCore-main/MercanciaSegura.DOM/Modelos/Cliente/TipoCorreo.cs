using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercanciaSegura.DOM.Modelos.Cliente
{
    [Table("Tipo_Correo")]
    public class TipoCorreo
    {
        [Key]
        [Column("Tipo_Correo_ID")]
        public int TipoCorreoId { get; set; }

        [Column("Tipo")]
        [MaxLength(100)]
        public string Tipo { get; set; }

    }
}
