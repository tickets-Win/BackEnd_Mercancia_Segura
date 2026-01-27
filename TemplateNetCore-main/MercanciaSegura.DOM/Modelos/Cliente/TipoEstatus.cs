using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercanciaSegura.DOM.Modelos.Cliente
{
    [Table("Tipo_Estatus")]
    public class TipoEstatus
    {
        [Key]
        [Column("Tipo_Estatus_ID")]
        public int EstatusId { get; set; }

        [Column("Tipo")]
        [MaxLength(100)]
        public string? Tipo { get; set; }
    }
}
