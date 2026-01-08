using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.DOM.Modelos
{
    [Table("Tipo_Cuenta")]
    public class TipoCuenta
    {
        [Key]
        [Column("Tipo_Cuenta_ID")]
        public int TipoCuentaId { get; set; }

        [Column("Tipo")]
        [MaxLength(100)]
        public string? Tipo { get; set; }
    }
}
