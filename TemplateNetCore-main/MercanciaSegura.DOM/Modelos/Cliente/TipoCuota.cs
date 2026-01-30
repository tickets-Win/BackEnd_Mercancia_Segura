using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercanciaSegura.DOM.Modelos.Cliente
{
        [Table("Tipo_Cuota")]
        public class TipoCuota
        {
            [Key]
            [Column("Tipo_Cuota_ID")]
            public int TipoCuotaId { get; set; }

            [Column("Tipo")]
            [MaxLength(100)]
            public string? Tipo { get; set; }

        }
    }
