using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.DOM.Modelos
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        [Column("Usuario_ID")]
        public int UsuarioId { get; set; }

        [Column("Usuario")]
        [MaxLength(50)]
        public string UsuarioNombre { get; set; } = null!;

        [Column("Password")]
        [MaxLength(255)]
        public string Password { get; set; } = null!;

        [Column("Correo")]
        [MaxLength(200)]
        public string Correo { get; set; } = null!;

        [Column("Fecha_Registro")]
        public DateTime FechaRegistro { get; set; }

        [Column("Estatus")]
        public bool Estatus { get; set; }
    }
}
