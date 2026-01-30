using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercanciaSegura.DOM.Modelos.Cliente
{
    [Table("Tipo_Tarifa")]
    public class TipoTarifa
    {
        [Key]
        [Column("Tipo_Tarifa_ID")]
        public int TipoTarifaId { get; set; }

        [Column("Tarifa")]
        [MaxLength(100)]
        public string Tarifa { get; set; }
    }
}
