using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.DOM.Modelos
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
