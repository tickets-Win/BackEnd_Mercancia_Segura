using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercanciaSegura.DOM.Modelos.Vendedor
{
    [Table("Vendedor")]
    public class Vendedor
    {
        [Key]
        [Column("Vendedor_ID")]
        public int VendedorId { get; set; }

        [Column("Apellido_Paterno")]
        [MaxLength(60)]
        public string? ApellidoPaterno { get; set; }

        [Column("Apellido_Materno")]
        [MaxLength(60)]
        public string? ApellidoMaterno { get; set; }

        [Column("Nombres")]
        [MaxLength(60)]
        public string? Nombres { get; set; }

        [Column("Nombre_Completo")]
        [MaxLength(120)]
        public string? NombreCompleto { get; set; }

        [Column("Tipo_Persona_ID")]
        public int? TipoPersonaId { get; set; }

        [Column("Tipo_Vendedor_ID")]
        public int? TipoVendedorId { get; set; }

        [Column("Estatus")]
        public bool Estatus { get; set; }

        [Column("Clave")]
        [MaxLength(10)]
        public string? Clave { get; set; }

        [Column("RFC")]
        [MaxLength(13)]
        public string? Rfc { get; set; }

        [Column("Domicilio")]
        [MaxLength(300)]
        public string? Domicilio { get; set; }

        [Column("CP")]
        [StringLength(5)]
        public string? Cp { get; set; }

        [Column("Colonia")]
        [MaxLength(60)]
        public string? Colonia { get; set; }

        [Column("Estado")]
        [MaxLength(20)]
        public string? Estado { get; set; }

        [Column("Genero")]
        [StringLength(1)]
        public string? Genero { get; set; }

        [Column("Telefono")]
        [MaxLength(13)]
        public string? Telefono { get; set; }

        [Column("Correo_Electronico")]
        [MaxLength(200)]
        public string? CorreoElectronico { get; set; }

        [Column("Observaciones")]
        [MaxLength(200)]
        public string? Observaciones { get; set; }

        [Column("Comision", TypeName = "decimal(5,2)")]
        public decimal? Comision { get; set; }


        [Column("Fecha_Actualizacion")]
        public DateTime? FechaActualizacion { get; set; }

        [Column("Fecha_Registro")]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [Column("Fecha_Baja")]
        public DateTime? FechaBaja { get; set; }

        //Llaves foraneas
        [ForeignKey(nameof(TipoVendedorId))]
        public TipoVendedor? TipoVendedor { get; set; }

        [ForeignKey(nameof(TipoPersonaId))]
        public TipoPersona? TipoPersona { get; set; }
    }
}
